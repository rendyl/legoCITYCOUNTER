# Lego CITY
Gamagora 2019 - Transformation d'un Mesh en Lego<br>
**Antoine CHEDIN & Rendy LATBI**

## Fonctionnalités implémentées :
- Grille Paramétrable pour l'Analyse du Mesh
- Analyse d'un Mesh et Export de la LegoMap en Json
- Creation d'un Mesh a partir d'une LegoMap

## Illustrations : 

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
- Choisir le fichier JSON crée dans son script associé VoxelGenerator
- Créer le Mesh

## Paramétrage de la Grille :
- Choisir la taille de la map (plaque de legos au sol).
- Choisir la scale et la height de la map pour qu'elle englobe tout le terrain à analyser.
- Choisir le LayerMask à intersecter (Everything ou bien un spécifique s'il ne faut pas analyser tout).
- L'option SetGroundAt0 permet de normaliser la hauteur du sol.
- Choisir le nom du fichier (sera situé dans Assets/JSON/).

## Détail des fichiers JSON exportés :
- Taille de la Map
- Echelle de la Map
- Nombre de Legos
- Matrice qui à chaque point associe son type et sa hauteur correspondante.
