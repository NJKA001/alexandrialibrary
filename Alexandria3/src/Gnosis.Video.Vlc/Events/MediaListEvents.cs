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
using System.Linq;
using System.Text;

using Gnosis.Video.Vlc.Media;

namespace Gnosis.Video.Vlc.Events
{
   public class MediaListItemAdded : EventArgs
   {
      public MediaListItemAdded(IVlcMedia item, int index)
      {
         Item = item;
         Index = index;
      }

      public IVlcMedia Item { get; private set; }
      public int Index { get; private set; }
   }

   public class MediaListWillAddItem : EventArgs
   {
      public MediaListWillAddItem(IVlcMedia item, int index)
      {
         Item = item;
         Index = index;
      }

      public IVlcMedia Item { get; private set; }
      public int Index { get; private set; }
   }

   public class MediaListItemDeleted : EventArgs
   {
      public MediaListItemDeleted(IVlcMedia item, int index)
      {
         Item = item;
         Index = index;
      }

      public IVlcMedia Item { get; private set; }
      public int Index { get; private set; }
   }

   public class MediaListWillDeleteItem : EventArgs
   {
      public MediaListWillDeleteItem(IVlcMedia item, int index)
      {
         Item = item;
         Index = index;
      }

      public IVlcMedia Item { get; private set; }
      public int Index { get; private set; }
   }

   public class MediaListPlayerNextItemSet : EventArgs
   {
      public MediaListPlayerNextItemSet(IVlcMedia item)
      {
         Item = item;
      }
      public IVlcMedia Item;
   }
}
