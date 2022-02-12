using GalleryManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalleryManager.Types;

public class ImporterInfo {
	public List<Media> Imports { get; set; }
	public Task Task { get; set; }
	public CancellationTokenSource TokenSource { get; set; }
	public FileCount FileCount { get; set; }

	public ImporterInfo() {
		Imports = new List<Media>();
		FileCount = new FileCount();
		TokenSource = new CancellationTokenSource();
	}
}
