using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryManager.Types;

public class FileCount {
	public int Processed { get; set; }
	public int Videos { get; set; }
	public int Pictures { get; set; }
	public int NewVideos { get; set; }
	public int NewPictures { get; set; }
	public int Total { get; set; }
}
