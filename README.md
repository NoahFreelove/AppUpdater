# AppUpdater

# Warning:
This is a C# library that uses a proprietary web app. Future plans may let you download from your own sources.

### About
This is the library and executable component to AppUpdater. It lets you easily download your app builds and update from a selected branch. This library also supports keys.

**This library currently only supports apps that are created using the AppUpdater Web App** which you can find [here](https://nfreelove-appupdater.web.app/)

### QuickStart (web)
1. Create an Account and Log In [here](https://nfreelove-appupdater.web.app/).
2. You should have one app credit, you can use this to create a new app by pressing the "Create New App" Button. Enter a name and continue.
3. You should be directed to the app-manager page which is where you can upload game builds to specific branches, create app keys, and view your app's ID.
4. Before anyone can download your app, you must upload it. You can do so by clicking the branch menu and pressing Upload New Build.
**You must send the app files themselves to zip, do not zip the parent folder and upload it. This will get more lenient as the updater gets more advanced.** 

### ~~Quick~~Start pt. 2 (desktop)
1. Import the App Updater DLL library (find the latest version in the github release section).
2. Extract the updater to it's own folder within your app. You will need the filepath to initialize the AppUpdater library.
3. Init your app with AppUpdater.Init() and fill in the parameters.
4. After a successful init, you can check for updates with Updater.CheckForUpdates(); and Download them with Downloader.DownloadUpdate()
5. You can setup a callback for when the update is done downloading with SetDownloadCompletedCallback()
6. Once an update has been downloaded, you can install it with Updater.StartUpdate(). This method will only update if an update is ready, is not currently downloading, and the app has initialized.
7. If everything is done right it should close your app, run updater.exe which will merge the new build with the current one, then upon completeion, re-launch your program.

