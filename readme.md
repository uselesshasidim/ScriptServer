# Overview

No more creating a ScriptCS files, copying it over to the server where it needs to run, and then log into the server to run it; or to setup a scheudle when the file should run.

When ScriptCS Server runs it watches a folder for ScriptCS files. When you drop (or ftp) a file into that folder, or if you change a file in that folder, the server will pick it up automatically and run it

On top of a ScriptCS file you can specify its run schedule or if you leave it out the server will run it once.

# Installing The Server
1. Download from 
1. Install

# Configuring The Server
In the folder where the server was installed you will find the app.config file, open that file to configure the server. Note: you will need to restart the service in order for the server to pickup the new config changes

1. WatchDirectory: The directory the server should watch for ScriptCS files

# Scheduling
You can specifiy a schedule when the ScriptCS file should run, by adding a timespan comment on top of the ScriptCS file
    
        //SCHEDULE-TIME-SPAN: [hour] [minutes] [seconds]
        //SCHEDULE-TIME-SPAN: 1 2 3
Means every 1 hour, 2 minutes and 3 seconds
        
        //SCHEDULE-TIME-SPAN: 0 2 3

Means every 2 minutes