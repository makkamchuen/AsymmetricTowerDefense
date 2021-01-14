# AsymmetricTowerDefense
An asymmetric tower defense mobile game using Unity framework

## Getting Started

1. Install [Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) and [Git LFS](https://git-lfs.github.com/).
2. Clone the repo using this command - `git clone https://github.com/makkamchuen/AsymmetricTowerDefense.git`
3. Open Project
4. Download fmodstudio20107.unitypackage (link in OneNote > BasicInfo > FMOD link)
5. Double click and import
6. Go to Navigation bar > FMOD Edit Settings
7. In the Inspector, choose `Multiple Platform Build`
8. Under `Build Path`, choose `AssetsFMOD` folder
9. `git reset --hard`
10. Hit play and make sure everything runs without error in console

## Check-in your code

1. Check out from dev
2. Create new branch when working on new feature 
3. Use pull request to update dev
4. When LFS prompts you for credentials, enter the username and password for LFS

## git-lfs @ AWS

* Please do not push media library contents into Asset folder since Amazon S3 is a paid storage
* Please only push those media files you used in the game.
* All media file extensions defined inside .gitattributes will automatically push to AWS S3 Storage when creating pull request
* Credentials are stored inside OneNote.
