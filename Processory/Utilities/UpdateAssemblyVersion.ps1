$version = git describe --tags --abbrev=0
$version = $version -replace '^v', ''  # Remove 'v' at the start of the version
$versionParts = $version -split '\.'
$major = $versionParts[0]
$minor = $versionParts[1]
$build = (Get-Date).ToString("yyMMdd")  # Use current date as build number
$revision = (Get-Date).ToString("HHmm")  # Use current time as revision number

$assemblyVersion = "$major.$minor.$build.$revision"
$fileVersion = $assemblyVersion  # Assuming file version is the same as assembly version
$informationalVersion = $version  # Use the original version tag as informational version

$content = Get-Content -Path "Properties/AssemblyInfo.cs"

# Process each line and replace only the necessary parts
$content = $content | ForEach-Object {
    $line = $_
    if ($line -match '\[assembly: AssemblyVersion\(".*"\)\]') {
        $line = $line -replace '\[assembly: AssemblyVersion\(".*"\)\]', "[assembly: AssemblyVersion(`"$assemblyVersion`")]"
    }
    if ($line -match '\[assembly: AssemblyFileVersion\(".*"\)\]') {
        $line = $line -replace '\[assembly: AssemblyFileVersion\(".*"\)\]', "[assembly: AssemblyFileVersion(`"$fileVersion`")]"
    }
    if ($line -match '\[assembly: AssemblyInformationalVersion\(".*"\)\]') {
        $line = $line -replace '\[assembly: AssemblyInformationalVersion\(".*"\)\]', "[assembly: AssemblyInformationalVersion(`"$informationalVersion`")]"
    }
    $line
}

# Write the updated content back to the file
$content | Set-Content -Path "Properties/AssemblyInfo.cs"
