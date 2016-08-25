param($installPath, $toolsPath, $package, $project)

$7zexe = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("7z.exe");
$7zdll = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("7z.dll");
$lzopexe = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("lzop.exe");
$lzo1dll = $project.ProjectItems.Item("Dependencies").ProjectItems.Item("lzo1.dll");
# Modify CopyOutputToDirectory to Copy If Newer.
$7zexe.Properties.Item("CopyToOutputDirectory").Value = [int]2;
$7zdll.Properties.Item("CopyToOutputDirectory").Value = [int]2;
$lzopexe.Properties.Item("CopyToOutputDirectory").Value = [int]2;
$lzo1dll.Properties.Item("CopyToOutputDirectory").Value = [int]2;