@page update Update

* **You should always have a look at the changelog before you update to a new version.**
* **Always make a backup before updating!**

# Update from a previous version
Most of the time you can simply update from an earlier version to a newer one, by just overwriting the source files.
If you encounter strange behaviors or errors, it might be a good idea to just delete the whole GameJolt API package and 
just reimport it again. If you do so, you will have to configure your API settings again (enter your game id and private key).

After updating you should test if everything works as expected. If not you should check the following:
* Are your API settings correct?
* Maybe the prefab references were not updated correctly. Try to remove the old GameJolt API object from the scene and add it again.
* Have a look at the changelog. Maybe the new version has introduced breaking changes. If so, have a look at the migration chapter below.
* Check out the forum, maybe someone else had a similar issue.

*Note: As with any asset package, any changes you made to the package will be lost on update, because the files will be overwritten.*

## Migration
While we try to be backwards compatible, it is sometimes inevitable to introduce breaking changes.
Such incompatible changes may vary in their complexity, it might be just a renamed method or and additional 
parameter, but it might also be a renamed class or a larger change in the underlying architecture. 
Please always have a look at the changelog before you update to a new version.

### API version 2.4.0
This version has renamed the two most fundamental classes.
Before this version there were two classes called `Manager`. One inside the `GameJolt.API` namespace and the other one 
in the `GameJolt.UI` namespace. This lead to some confusion and therefore I decided to rename these files from `Manager`
to `GameJoltAPI` and `GameJoltUI`. The namespaces are still the same, so the fully qualified name looks a bit 
strange and redundant (`GameJolt.API.GameJoltAPI`), but since most of the time you're just using the `using` directive, 
this doesn't really matter. 

This change has also the benefit that you can now use both classes from within the same file without having to use the 
fully qualified name.

Because of this change you're code will probably no longer work, because you are still referencing the `Manager` classes, 
which no longer exist under that name. So your development environment will show several errors like this:
* The name 'Manager' does not exist in the current context
* The type or namespace name 'Manager' does not exist in the namespace 'GameJolt.API' (are you missing an assembly reference?)
* 'Manager' does not contain a definition for '...'
* 'Manager' does not contain a definition for '...' and no extension method '...' accepting a first argument of type 
'Manager' could be found (are you missing a using directive or an assembly reference?)
* ...

To fix these issues, you just have to change all occurrences of the old identifier with the new one.
If you were always using the fully qualified name, you can just do a 'search&replace' within your development environment.
For example you could just search for `GameJolt.API.Manager` and replace it with `GameJolt.API.GameJoltAPI`.
