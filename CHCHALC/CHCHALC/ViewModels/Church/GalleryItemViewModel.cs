using System;

using CHCHALC.Models;

namespace CHCHALC.ViewModels
{
    public class GalleryItemViewModel : BaseViewModel
    {
        public PlayItem Item { get; set; }

        public GalleryItemViewModel(PlayItem item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
