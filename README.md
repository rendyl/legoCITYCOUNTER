# Lego CITY
Gamagora 2019 - Transformation d'un Mesh en Lego<br>
**Antoine CHEDIN & Rendy LATBI**

## Fonctionnalités implémentées :
- Grille Paramétrable pour l'Analyse du Mesh
- Analyse d'un Mesh et Export de la LegoMap en Json
- Creation d'un Mesh a partir d'une LegoMap

## Illustrations : 
<img src="Assets/Img/Terrain.PNG" data-canonical-src="Assets/Img/Terrain.PNG" width="430" height="250" /> <img src="Assets/Img/Lego.PNG" data-canonical-src="Assets/Img/Lego.PNG" width="430" height="250" />

## Utilisation du Projet :
- Importer le Projet sur votre PC
- Le lancer avec Unity 2019.2.x
- Ouvrir la Scène Lego.unity
- Importer votre modèle 3D à Analyser dans Unity
- Vérifier que celui-ci possède bien des Colliders (optionnel : ajoutez le tag "Ground" à votre sol)
- Deplacer le GameObject LegoAnalyser sur votre modèle 3D
- Paramétrer la Grille (script associé LegoAnalyser) > cf section appropriée
- Exporter en JSON
- Selectionner le GameObject VoxelGenerator
- Paramétrer le générateur avec votre JSON
- Créer le Mesh

## Paramétrage de la Grille (LegoAnalyser) :
<img src="Assets/Img/LegoAnalyser.PNG" data-canonical-src="Assets/Img/LegoAnalyser.PNG" width="430" height="130" /><br>
- Choisir la taille de la map (plaque de legos au sol).
- Choisir la scale et la height de la map pour qu'elle englobe tout le terrain à analyser.
- Choisir le LayerMask à intersecter (Everything ou bien un spécifique s'il ne faut pas analyser tout).
- L'option SetGroundAt0 permet de normaliser la hauteur du sol.
- Choisir le nom du fichier (sera situé dans Assets/JSON/).

## Détail des fichiers JSON exportés :
<img src="Assets/Img/JSON.PNG" data-canonical-src="Assets/Img/JSON.PNG" width="300" height="250" /><br>
- Taille de la Map
- Echelle de la Map
- Nombre de Legos
- Matrice qui à chaque point associe son type et sa hauteur correspondante.
