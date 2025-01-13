# Projet

Ce projet a été réalisé dans le cadre d'un test et nécessitait de faire deux choses:
- une application console permettant à des dresseurs de faire faire de tours à des singes
- une api permettant de "manipuler" des personnes

Ce project contient deux sous projets:
- "Circus" : pour la partie avec les singes
- "People-CRUD" : pour le services web permettant de faire diverses opération sur des personnes

## Importants

### Démarrage du projet

Avant de lancer le project, il faut créer un nouveau fichier "appsettings.json" dans le projet "People.API" et y ajouter cette section:
```sh
"ConnectionStrings": {
  "DefaultConnection": "Data source=people.db"
},
```

### Base de données

Un fichier "people.db" est présent pour la base de données. Donc il ne devrait pas être nécessaire de créer la base de données pour le projet mais si un problème venait à survenir avec cette dernière, voici ce que vous pouvez faire:
- Supprimer le fichier "" qui se trouve dans le dossier du projet "people.db"
- Ouvrir le "Package Manager Console" pour le projet "People.API" et exécuter la command "Update-Database" (attention, dans ce cas là, la base de données sera vide)

### Utilisation

Pour utiliser l'api, il faut se munir de postman et importer la collection du fichier "People.postman_collection.json".