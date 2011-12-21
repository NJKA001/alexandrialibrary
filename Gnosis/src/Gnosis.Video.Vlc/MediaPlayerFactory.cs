//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Gnosis.Video.Vlc.Exceptions;
using Gnosis.Video.Vlc.Interop;
using Gnosis.Video.Vlc.Manager;
using Gnosis.Video.Vlc.Media;
using Gnosis.Video.Vlc.Players;

namespace Gnosis.Video.Vlc
{
    /// <summary>
    /// Entry point for the nVLC library.
    /// </summary>
    public class MediaPlayerFactory : DisposableBase, IMediaPlayerFactory, IReferenceCount, INativePointer
    {
        IntPtr m_hMediaLib = IntPtr.Zero;
        private readonly ILogger logger;
        Log m_log;
        IVideoLanManager m_vlm = null;

        /// <summary>
        /// Initializes media library with default arguments
        /// </summary>
        public MediaPlayerFactory(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;

            string[] args = new string[] 
             {
                "-I", 
                "dumy",  
		        "--ignore-config", 
                "--no-osd",
                "--disable-screensaver",
               // "--ffmpeg-hw",
		        "--plugin-path=./plugins" 
             };

            Initialize(args);
        }

        /// <summary>
        /// Initializes media library with user defined arguments
        /// </summary>
        /// <param name="args">Collection of arguments passed to libVLC library</param>
        public MediaPlayerFactory(ILogger logger, string[] args)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;

            Initialize(args);
        }

        private void Initialize(string[] args)
        {
            try
            {
                m_hMediaLib = LibVlcMethods.libvlc_new(args.Length, args);
            }
            catch (DllNotFoundException ex)
            {
                throw new LibVlcNotFoundException(ex);
            }

            if (m_hMediaLib == IntPtr.Zero)
            {
                throw new LibVlcInitException();
            }

            m_log = new Log(m_hMediaLib, logger);
            m_log.Enabled = true;
        }

        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <typeparam name="T">Type of the player to create</typeparam>
        /// <returns>Newly created player</returns>
        public T CreatePlayer<T>() where T : IVlcPlayer
        {
            return ObjectFactory.Build<T>(m_hMediaLib);
        }

        /// <summary>
        /// Creates new instance of media list player
        /// </summary>
        /// <typeparam name="T">Type of media list player</typeparam>
        /// <param name="mediaList">Media list</param>
        /// <returns>Newly created media list player</returns>
        public T CreateMediaListPlayer<T>(IMediaList mediaList) where T : IVlcMediaListPlayer
        {
            return ObjectFactory.Build<T>(m_hMediaLib, mediaList);
        }

        /// <summary>
        /// Creates new instance of media.
        /// </summary>
        /// <typeparam name="T">Type of media to create</typeparam>
        /// <param name="input">The media input string</param>
        /// <param name="options">Optional media options</param>
        /// <returns>Newly created media</returns>
        public T CreateMedia<T>(string input, params string[] options) where T : IVlcMedia
        {
            T media = ObjectFactory.Build<T>(m_hMediaLib);
            media.Input = input;
            media.AddOptions(options);

            return media;
        }

        /// <summary>
        /// Creates new instance of media list.
        /// </summary>
        /// <typeparam name="T">Type of media list</typeparam>
        /// <param name="mediaItems">Collection of media items</param>       
        /// <param name="options">Media options applied on every media item in the list</param>
        /// <returns>Newly created media list</returns>
        public T CreateMediaList<T>(IEnumerable<string> mediaItems, params string[] options) where T : IMediaList
        {
            T mediaList = ObjectFactory.Build<T>(m_hMediaLib);
            foreach (var file in mediaItems)
            {
                mediaList.Add(this.CreateMedia<IVlcMedia>(file, options));
            }

            return mediaList;
        }

        /// <summary>
        /// Creates media list instance with no media items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateMediaList<T>() where T : IMediaList
        {
            return ObjectFactory.Build<T>(m_hMediaLib);
        }

        /// <summary>
        /// Gets the libVLC version.
        /// </summary>
        public string Version
        {
            get
            {
                return LibVlcMethods.libvlc_get_version();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Release();
        }

        private static class ObjectFactory
        {
            static Dictionary<Type, Type> objectMap = new Dictionary<Type, Type>();

            static ObjectFactory()
            {
                objectMap.Add(typeof(IMedia), typeof(BasicMedia));
                objectMap.Add(typeof(IVlcMediaFromFile), typeof(MediaFromFile));
                objectMap.Add(typeof(IVideoInputMedia), typeof(VideoInputMedia));
                objectMap.Add(typeof(IScreenCaptureMedia), typeof(ScreenCaptureMedia));
                objectMap.Add(typeof(IVlcPlayer), typeof(BasicPlayer));
                objectMap.Add(typeof(IVlcAudioPlayer), typeof(AudioPlayer));
                objectMap.Add(typeof(IVlcVideoPlayer), typeof(VideoPlayer));
                objectMap.Add(typeof(IVlcDiskPlayer), typeof(DiskPlayer));
                objectMap.Add(typeof(IMediaList), typeof(MediaList));
                objectMap.Add(typeof(IVlcMediaListPlayer), typeof(MediaListPlayer));
                objectMap.Add(typeof(IVideoLanManager), typeof(VideoLanManager));
            }

            public static T Build<T>(params object[] args)
            {
                if (objectMap.ContainsKey(typeof(T)))
                {
                    return (T)Activator.CreateInstance(objectMap[typeof(T)], args);
                }

                throw new ArgumentException("Unregistered type", typeof(T).FullName);
            }
        }

        #region IReferenceCount Members

        public void AddRef()
        {
            LibVlcMethods.libvlc_retain(m_hMediaLib);
        }

        public void Release()
        {
            try
            {
                LibVlcMethods.libvlc_release(m_hMediaLib);
            }
            catch (AccessViolationException)
            { }
        }

        #endregion

        #region INativePointer Members

        public IntPtr Pointer
        {
            get
            {
                return m_hMediaLib;
            }
        }

        #endregion

        /// <summary>
        /// Gets list of available audio filters
        /// </summary>
        public IEnumerable<FilterInfo> AudioFilters
        {
            get
            {
                IntPtr pList = LibVlcMethods.libvlc_audio_filter_list_get(m_hMediaLib);
                libvlc_module_description_t item = (libvlc_module_description_t)Marshal.PtrToStructure(pList, typeof(libvlc_module_description_t));

                do
                {
                    yield return GetFilterInfo(item);
                    if (item.p_next != IntPtr.Zero)
                    {
                        item = (libvlc_module_description_t)Marshal.PtrToStructure(item.p_next, typeof(libvlc_module_description_t));
                    }
                    else
                    {
                        break;
                    }

                }
                while (true);

                LibVlcMethods.libvlc_module_description_list_release(pList);
            }
        }

        /// <summary>
        /// Gets list of available video filters
        /// </summary>
        public IEnumerable<FilterInfo> VideoFilters
        {
            get
            {
                IntPtr pList = LibVlcMethods.libvlc_video_filter_list_get(m_hMediaLib);
                libvlc_module_description_t item = (libvlc_module_description_t)Marshal.PtrToStructure(pList, typeof(libvlc_module_description_t));

                do
                {
                    yield return GetFilterInfo(item);
                    if (item.p_next != IntPtr.Zero)
                    {
                        item = (libvlc_module_description_t)Marshal.PtrToStructure(item.p_next, typeof(libvlc_module_description_t));
                    }
                    else
                    {
                        break;
                    }

                }
                while (true);

                LibVlcMethods.libvlc_module_description_list_release(pList);
            }
        }

        private FilterInfo GetFilterInfo(libvlc_module_description_t item)
        {
            return new FilterInfo()
            {
                Help = Marshal.PtrToStringAnsi(item.psz_help),
                Longname = Marshal.PtrToStringAnsi(item.psz_longname),
                Name = Marshal.PtrToStringAnsi(item.psz_name),
                Shortname = Marshal.PtrToStringAnsi(item.psz_shortname)
            };
        }

        /// <summary>
        /// Gets the VLM instance
        /// </summary>
        public IVideoLanManager VideoLanManager
        {
            get
            {
                if (m_vlm == null)
                {
                    m_vlm = ObjectFactory.Build<IVideoLanManager>(m_hMediaLib);
                }

                return m_vlm;
            }
        }
    }
}
