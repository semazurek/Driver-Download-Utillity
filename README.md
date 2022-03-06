# Driver-Download-Utillity
Application created for Windows 10/11, looking for drivers that were not installed for some reason.

(Like couldn't find by windows update etc.)

Download link: <a href="https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/semazurek/Driver-Download-Utillity/blob/main/DDU.exe"> DDU.exe </a>

## How it works

1) The program extracts data about the name and instance id of the driver,
```
3D Video Controller
PCI\VEN_10DE&DEV_1C8D&SUBSYS_3...
```

2) Converts device instance id data to VEN_ and DEV_ value,
```
3D Video Controller
VEN_10DE DEV_1C8D
```

3) Then redirect to the download-drivers.net website with the entered converted id data.
```
download-drivers.net/search?q=VEN_10DE%26DEV_1C8D
```

## First look

<img src="https://user-images.githubusercontent.com/85984736/156902812-c67b44bc-e10f-4382-bf1b-f426923d5500.png" width="760">

<img src="https://user-images.githubusercontent.com/85984736/156903232-dd130ead-ddce-4e4f-9bab-e295ade4da16.png" width="760">

## How to install downloaded drivers

### Standard installation (Setup.exe)

<img src="https://user-images.githubusercontent.com/85984736/156904240-f8de27c9-1772-41db-96c1-5bde46f7d669.png" width="760">

```
Double click on setup.exe, installation.exe or other setup utility with .exe extension
```

### Advanced installation without .exe file via Device Manager (.Inf Files)

<img src="https://user-images.githubusercontent.com/85984736/156904232-bf01ca66-3998-4793-b23c-652ee6b4cde6.gif" width="760">

```
Device Manager -> Right Click on Driver -> Update Driver -> Browse my computer for drivers -> Browse -> Select folder -> Next.
```
