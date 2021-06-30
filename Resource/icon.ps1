﻿Param(
	[string] $FirstInput = '',
	[switch] $BatchMode
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$currentDirPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$rootDirPath = Split-Path -Parent $currentDirPath
$iconDirPath = Join-Path $currentDirPath 'Icon'
$workDirPath = Join-Path $iconDirPath '@work'

$exeIncspace = if ($env:INKSCAPE) { $env:INKSCAPE } else { [Environment]::ExpandEnvironmentVariables('C:\Program Files\Inkscape\bin\inkscape.exe') }
$exeImageMagic = if ($env:IMAGEMAGIC) { $env:IMAGEMAGIC } else { [Environment]::ExpandEnvironmentVariables('C:\Applications\ImageMagick\convert.exe') }

$appIcons = @(
	@{
		name  = 'App-release'
		color = '#ffffff'
	}
)
$iconSize = @(
	16, 18, 20, 24, 28
	32, 40
	48, 56, 60, 64
	72, 80, 84, 96, 112, 128, 160
	192, 224
	256, 320, 384, 448, 512
)




function ConvertAppSvgToPng() {
	# App.svg から release(そのまま), debug, beta を生成
	$appPath = Join-Path $iconDirPath 'App.svg'
	$appXml = [xml](Get-Content $appPath -Raw -Encoding UTF8)

	$nodes = @(
		$appXml.SelectSingleNode('//*[@id="backgroundLinearGradientStart"]')
		$appXml.SelectSingleNode('//*[@id="backgroundLinearGradientEnd"]')
	)

	New-Item -Path $workDirPath -ItemType Directory -Force | Out-Null

	$savedPaths = @()
	foreach ($appIcon in $appIcons) {
		foreach ($node in $nodes) {
			$style = $node.Attributes['style'].Value
			$node.Attributes['style'].Value = $style -replace "stop-color:(#[0-9a-f]{6})", "stop-color:$($appIcon.color)"
		}
		$savePath = Join-Path $workDirPath "$($appIcon.name).svg"
		$appXml.Save($savePath)
		$savedPaths += $savePath
	}
	foreach ($savePath in $savedPaths) {
		ConvertSvgToPng "$savePath"
	}
}

function PackAppIcon {
	foreach ($appIcon in $appIcons) {
		$pattern = "$($appIcon.name)_*.png"
		$outputPath = Join-Path $iconDirPath "$($appIcon.name).ico"
		PackIcon $workDirPath $pattern $outputPath
	}
}

function ConvertSvgToPng([string] $srcSvgPath) {
	$pngBasePath = Join-Path (Split-Path -Parent $srcSvgPath) ([System.IO.Path]::GetFileNameWithoutExtension($srcSvgPath))
	Write-Host "[SRC] $srcSvgPath";
	foreach ($size in $iconSize) {
		$pngPath = "${pngBasePath}_${size}.png"
		Write-Host "   -> $pngPath"
		# 1回じゃ無理なんで2回やるべし。待機も試したけどなんかダメだった
		& $exeIncspace `
			--export-dpi=96 `
			--export-width=$size `
			--export-height=$size `
			--export-filename="$pngPath" `
			--export-overwrite `
			--batch-process `
			--export-type=png `
			--export-filename="$pngPath" `
			"$srcSvgPath"
		Start-Sleep -Seconds 1
	}
}

function PackIcon([string] $directoryPath, [string] $pngPattern, [string] $outputPath) {
	Write-Host "$directoryPath $pngPattern"
	$inputFiles = (
		Get-ChildItem -Path $directoryPath -Filter $pngPattern -File `
		| Select-Object -ExpandProperty FullName `
		| Sort-Object { [regex]::Replace($_, '\d+', { $args[0].Value.PadLeft(20) }) }
	)
	Write-Host $inputFiles
	& $exeImageMagic $inputFiles $outputPath
}

function MoveAppIcon {
	$mainDir = Join-Path $rootDirPath 'Source\Pe\Pe.Main\Resources\Icon'
	foreach ($appIcon in $appIcons) {
		$srcPath = Join-Path $currentDirPath "$($appIcon.name).ico"
		$dstPath = Join-Path $mainDir "$($appIcon.name).ico"
		Write-Host "[COPY] $srcPath -> $dstPath"
		Copy-Item -Path $srcPath -Destination $dstPath
	}

}

while ($true) {
	Write-Host "1: Pe: SVG -> PNG"
	Write-Host "2: Pe: PNG -> ICO"
	Write-Host "x: 終了"
	if ($FirstInput) {
		$inputValue = $FirstInput
		$FirstInput = ''
	}
 else {
		$inputValue = Read-Host "処理"
	}
	try {
		switch ($inputValue) {
			'1' {
				ConvertAppSvgToPng
			}
			'2' {
				PackAppIcon
			}
			'x' {
				exit 0;
			}
			default {
				Write-Host "[$inputValue] は未定義"
			}
		}
	}
 catch {
		Write-Host $Error[0] -ForegroundColor Red -BackgroundColor Black
	}
	Write-Host ''
	if ($BatchMode) {
		exit 0
	}
}


