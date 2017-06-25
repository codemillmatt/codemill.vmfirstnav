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
        Version = "2.3.0.49"
    };
    
    var deps = new List<NuSpecDependency>();
    deps.Add(formsDependency);

	NuGetPack ("./VMFirstNav.nuspec", new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = "./",
		BasePath = "./",
        Dependencies = deps
	});	
});


RunTarget (TARGET);