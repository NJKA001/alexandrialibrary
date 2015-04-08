# Alexandria Media Library #

## Overview ##
Alexandria is a loosely coupled suite of media applications. It is written in C# and designed with a plugin-based architecture so that everything from the the playback engine to the database storage can be swapped out and configured to meet a user's needs.  The core Alexandria libraries are released under the MIT license.  Each plugin has its own license.

## Features ##
The architectue of Alexandria allows plugins to be designed to perform any function imaginable and then integrate these with the existing plugins to provide a full application.
Plugins exist for:
  * media playback
  * create, manage and share playlists
  * tag media files with metadata
  * search for media and metadata
  * share information about your taste

## Plugins ##
Examples of plugins include:
  * [MusicBrainz](http://en.wikipedia.org/wiki/MusicBrainz) - lookup and submit music metadata
  * [MusicDNS](http://en.wikipedia.org/wiki/MusicDNS) - use acoustic fingerprinting to identify your audio files
  * [Last.fm](http://en.wikipedia.org/wiki/Lastfm) - submit tracks and access your profile
  * [MP3tunes](http://en.wikipedia.org/wiki/MP3tunes) - access your online music locker
  * [Amazon](http://en.wikipedia.org/wiki/Amazon.com) - lookup movie and music metadata
  * [FMOD](http://en.wikipedia.org/wiki/FMOD) - audio playback
  * [SQLite](http://en.wikipedia.org/wiki/SQLite) - store your media library