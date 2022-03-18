string devEnv = @"devenv";
string projectPath = @"%USERNAME%\Downloads\MagniClass\MagniClass.sln" ;
string compileProject = string.Format("/c \"{0}\" /run \"{1}\"",
    devEnv, projectPath);
Process.Start(new ProcessStartInfo{FileName = "cmd", Arguments = compileProject});