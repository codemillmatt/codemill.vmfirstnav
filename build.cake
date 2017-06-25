#addin "Cake.FileHelpers"

var TARGET = Argument ("target", Argument ("t", "Default"));

var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "0.0.9999");

Task ("Default").Does (() =>
{
	NuGetRestore ("./VMFirstNav.sln");

	DotNetBuild ("./VMFirstNav.sln", c => c.Configuration = "Release");
});

Task ("NuGetPack")
	.IsDependentOn ("Default")
	.Does (() =>
{
    var formsDependency = new NuSpecDependency {
        Id = "Xamarin.Forms",
        Version = "2.3.4.247"
    };
    
    var deps = new List<NuSpecDependency>();
    deps.Add(formsDependency);

	var contentFile = new NuSpecContent {
		Source = @"VMFirstNav\bin\Release\netstandard1.2\codemill.VMFirstNav.*",
		Target = @"lib\netstandard1.2"
	};

	var files = new List<NuSpecContent>();
	files.Add(contentFile);

	NuGetPack (new NuGetPackSettings { 
		Id = "CodeMill.VMFirstNav",
		Authors = new List<string> {"Matthew Soucoup"},
		Version = version,
		Title = "View Model First Navigation for Xamarin.Forms",
		Description="Perform navigation from view models in Xamarin.Forms without having to pass in the navigation object into the VM layer.",
		Owners = new List<string> { "codemillmatt" },
		ProjectUrl = new Uri("https://github.com/codemillmatt/codemill.vmfirstnav"),
		LicenseUrl = new Uri("https://github.com/codemillmatt/codemill.vmfirstnav/blob/master/LICENSE"),
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = ".",
		BasePath = ".",
        Dependencies = deps,
		ReleaseNotes = new List<string> { "Visit https://github.com/codemillmatt/codemill.vmfirstnav/changelog.md to see list of changes." },
		Files = files
	});	
});


RunTarget (TARGET);