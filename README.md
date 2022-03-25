# Driver-Download-Utillity

<a href="#"><img src="https://img.shields.io/badge/RELEASE-v1.6-orange?style=for-the-badge&"></a>
<a href="#"><img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white"></a>
<a href="#"><img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"></a>
<a href="#"><img src="https://img.shields.io/badge/powershell-5391FE?style=for-the-badge&logo=powershell&logoColor=white"></a>
<a href="https://paypal.me/rikey" target="_blank"><img src="https://img.shields.io/badge/PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white"></a>

Application created for Windows 10/11, looking for drivers that were not installed for some reason.

(Like couldn't find by windows update etc.)

Download link: <a href="https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/semazurek/Driver-Download-Utillity/blob/main/DDU.exe"> DDU.exe </a>

## What it does

1) Scan for drivers with error/problem status by powershell command:
```
Get-PnpDevice -Status ERROR | select FriendlyName -ExpandProperty Name | ft -hide
```
```
Get-PnpDevice -Status ERROR | select InstanceId | findstr /c:VEN_ /c:VID_
```

2) Extracts data about the name and instance id of the driver:
```
3D Video Controller
PCI\VEN_10DE&DEV_1C8D&SUBSYS_3...
```

3) Converts device instance id data to VEN_ and DEV_ (PCI) or VID_ and DIV_ (USB_HDI) value:
```
3D Video Controller
VEN_10DE DEV_1C8D
```
4) Checks on the type of selected driver to download for correctly convert its ID

5) Then redirect to the download-drivers.net website with the entered converted id data:
```
download-drivers.net/search?q=VEN_10DE%26DEV_1C8D
```

## First look

<img src="https://user-images.githubusercontent.com/85984736/156903232-dd130ead-ddce-4e4f-9bab-e295ade4da16.png" width="760">

## How to install downloaded drivers

### Standard installation (Setup.exe)

```
Double click on setup.exe, installation.exe or other setup utility with .exe extension
```

### Advanced installation without .exe file via Device Manager (.Inf Files)

```
Device Manager -> Right Click on Driver -> Update Driver -> Browse my computer for drivers -> Browse -> Select folder -> Next.
```
