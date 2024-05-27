# GizmoExtensionPro

![AppVeyor](https://img.shields.io/badge/build-passing-brightgreen)

![gizmoPro](https://github.com/2Kerfur/GizmoExtensionPro/assets/73479696/d2301b32-5af9-4f1d-921a-2021448e1011)

## Info

* [About](#about)
* [Setup](#setup)
* [Examples](#examples)
* [License](#license)

## About
This project made for simplification of level creation.
Tested on Unity 2020.3 (Windows).

## Setup
To run this project you need Unity version 2018.2 or higher.
1. Download project code and unpack it.
2. In Unity Editor open Assets folder and paste Utils folder.
3. Rebuld C# project (it might be rebuild automatically).
7. Enjoy :)
   
## Examples 
```cs
Utils.GizmoPro.DrawWireIcosphere(new Vector3(0, 0, 0), new Vector3(1, 1, 1), Quaternion.identity);
Utils.GizmoPro.DrawWireCube(new Vector3(3, 0, 0), new Vector3(1, 1, 1), Quaternion.identity);
Utils.GizmoPro.DrawWireCapsule(new Vector3(6, 0, 0), Quaternion.identity, 1, 4);
Utils.GizmoPro.DrawIcosphere(new Vector3(9, 0, 0), new Vector3(1, 1,1), Quaternion.identity);
Utils.GizmoPro.DrawWireSphere(new Vector3(12, 0, 0), 1f, Quaternion.identity);

Utils.GizmoPro.DrawWireCircle(new Vector3(0, 0, 3), 1f, Quaternion.identity);
Utils.GizmoPro.DrawWireCylinder(new Vector3(3, 0, 3), 1f, 2f, Quaternion.identity);
Utils.GizmoPro.DrawWireCross(new Vector3(6, 0, 3), 2f);
Utils.GizmoPro.DrawWireArrow(new Vector3(9, 0, 3), new Vector3(9, 0, 3) + transform.up*2, 0.25f, 20, 3);
Utils.GizmoPro.DrawWireLine(new Vector3(12, 0, 3), new Vector3(12, 0, 3) + transform.up * 2);
```
## License
Unity GizmoExtensionPro licensed under the Apache 2.0 License, see LICENSE for more information.
