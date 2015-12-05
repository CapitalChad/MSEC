$urls = @();
# TODO: Change this URL based on the target environment
$urls = $urls + "http://sp2010:7500";
# TODO: Add additional site URLs using the syntax of the previous line

$prefix = "c:0-.f|msecpersonifyroleprovider|";

$ownerRoles = @();
#$ownerRoles = $ownerRoles + "test5";
# TODO: Add any roles required

$contributorRoles = @();
# TODO: Add any roles required

$visitorRoles = @();
# TODO: Add any roles required

foreach ($url in $urls) {
	Write-Host Working on web $url;
	$web = Get-SPWeb $url;

	$ownerGroup = $web.Groups[$web.Title + " Owners"];
	foreach ($role in $ownerRoles) {
		$loginName = $prefix + $role;
		Write-Host Adding role $role with login $loginName as an Owner;
		$web.AllUsers.Add($loginName, "", $role, $role);
		$web.Update();
		$ownerGroup.AddUser($web.AllUsers[$loginName]);
		$ownerGroup.Update();
		$web.Update();
	}

	$contributorGroup = $web.Groups[$web.Title + " Members"];
	foreach ($role in $contributorRoles) {
		$loginName = $prefix + $role;
		Write-Host Adding role $role with login $loginName as a Contributor;
		$web.AllUsers.Add($loginName, "", $role, $role);
		$web.Update();
		$contributorGroup.AddUser($web.AllUsers[$loginName]);
		$contributorGroup.Update();
		$web.Update();
	}

	$visitorGroup = $web.Groups[$web.Title + " Visitors"];
	foreach ($role in $visitorRoles) {
		$loginName = $prefix + $role;
		Write-Host Adding role $role with login $loginName as a Visitor;
		$web.AllUsers.Add($loginName, "", $role, $role);
		$web.Update();
		$visitorGroup.AddUser($web.AllUsers[$loginName]);
		$visitorGroup.Update();
		$web.Update();
	}
}
