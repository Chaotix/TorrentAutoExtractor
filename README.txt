Since i couldn't find anything that would auto-extract when a torrent was down downloading, I made a program that monitors my torrent directory, extracts movies and TV shows to a default location and renames the file to the appropriate name(does great 99% of the time with renaming).  It doesn’t install anything it just runs as a standalone EXE and throws a config file in your users appdata folder. 

Version 1.1
See below for change log

The details: 
-it’s made in Visual Basic, you need UNRAR.EXE & .Net installed, and I’ve only tested it on Windows 7/Vista 32&64bit. Even after months of running and not rebooting, it doesn’t throw errors or use up much memory.  It minimizes or hides to the system icon tray so it’ll stay out of the way and do its job.
-YOU MUST have your torrent program move the torrent files, once it’s done downloading, to a completed directory, and have the program monitor the completed folder for torrents.
-It has a queue, however it only extracts one torrent at a time, as to not noticeably slow the system.  However 90% of the time it never has to use the queue since the torrents extract pretty quick.
-If a video isn’t in a rar it will just COPY it from the torrent directory, if it’s in a RAR it’ll unrar it and move the files to the appropriate folders. 
-If you have the folder structure setup(TV SHOW NAME\Season #), it'll put TV shows in their correct folder/season as well as rename it appropriately.  If it doesn’t find the TV Show name folder, it’ll dump the file into a default directory, and renamed appropriately.
- For movies, it just dumps them into a default directory and renamed appropriately.
-The renaming is based off the Torrent name, not the rar’d file name because the torrent name typically has more information and isn’t summarized.  The Naming convention has different fields Movies(Name Year HD), TV shows(Name Season Episode Title HD) that can you move around or delete to fit your needs or even add things into there, say if you wanted an E infront of the TV shows or Dots instead of Dashes with spaces.  Ex: “E.{Name}.{Title}.{Season}.{Episode}.{HD}” it just replaces what’s in the brackets

IE:
The.Walking.Dead.S02E04.HDTV.XviD-ASAP = The Walking Dead - S01 - 04
Transformers.Prime.S01E26.One.Shall.Rise.Pt.3.720p.WEB-DL-Reaperza = Transformers Prime - S01 - 26 - One Shall Rise Pt 3 - HD 720p  
Conan.The.Barbarian.2011.BDRip.XviD.AC3-FUSiON = Conan The Barbarian 2011
Horrible.Bosses.Extended 2011 BDRip XvidHD 720P-NPW = Horrible Bosses Extended 2011 - HD 720p

Other Features:
-Double click a torrent to open the directory it’s located
-Right click a torrent to: Play, open source directory, change group, or remove the torrent from the list
-Right Click the system icon and it can add itself into windows startup or remove itself(tries registry first, then tries startmenu)
-Groups the torrent (Completed, Error, Queried, Extracting, Other) Other is for if you don’t have the Process All torrents boxed checked, it will try and determine if it’s a TV or MOVIE, if it’s not it won’t try and extract it and it’ll be put into this group. From there you can remove or if it IS a movie, r-click change group and add it back into the queue and it’ll process the torrent.
-Can use window notifications
-Look for new torrents button will look at the newest torrent it has loaded, and see if there’s any newer torrents in the directory to load. (incase it wasn’t running and you decide to load it up after you downloaded 20+ new movies that you don’t want to manually extract)
-Override toggle – The program checks to see if the file name exists, if it does the new file will override the old one if this is checked.  If not then the program will still move the movie/tv show, just not rename the file.
-Cleanup extracted files Toggle – If checked it will remove any leftover files that were in the rar and removes the directory they were extracted to(by default they are extracted to “(TorrentNameDir)\Extracted Files”)

Other notes: After my son decided to hit my power button on the computer about 10 times, Windows BSOD and the program’s settings file got corrupted and I could no longer open the program. Since then I’ve added checks to opening the program, and it will make a backup of the config file each time it opens if it’s a valid file(over-rides last backup). So now if it has an error in the config, it will load from the backup file, so you won’t have to redo your settings and you’ll still have most of your torrents loaded.

Also there are a couple hidden Columns at right side of the Status column, "When file was created" and "Path of Extracted FIles".  The other 2 Date & Time columns are the last time the program did something with that entry, so those can change.  The Path of Extracted files will show the path and the name of the file, if there was more than one file moved it'll just show the path.

Changes:
Ver 1.1 :
 - Added Column for destination folder and name(hidden at end of column list)
- Changed Double-Click to open Destination Directory vs Source
- Added Play and open Source Dir to Right-Click menu
-Makes a backup of the config file each time the program is open, in case of corrupt config file
-Checks for TV shows first, using the TV filter string in the advanced settings
-a few other small misc items that I can’t remember…


Let me know what you think and what I need to change.

