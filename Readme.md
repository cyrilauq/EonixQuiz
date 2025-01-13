# Projet

Ce projet a �t� r�alis� dans le cadre d'un test et n�cessitait de faire deux choses:
- une application console permettant � des dresseurs de faire faire de tours � des singes
- une api permettant de "manipuler" des personnes

Ce project contient deux sous projets:
- "Circus" : pour la partie avec les singes
- "People-CRUD" : pour le services web permettant de faire diverses op�ration sur des personnes

## Importants

### D�marrage du projet

Avant de lancer le project, il faut cr�er un nouveau fichier "appsettings.json" dans le projet "People.API" et y ajouter cette section:
```sh
"ConnectionStrings": {
  "DefaultConnection": "Data source=people.db"
},
```

### Base de donn�es

Un fichier "people.db" est pr�sent pour la base de donn�es. Donc il ne devrait pas �tre n�cessaire de cr�er la base de donn�es pour le projet mais si un probl�me venait � survenir avec cette derni�re, voici ce que vous pouvez faire:
- Supprimer le fichier "" qui se trouve dans le dossier du projet "people.db"
- Ouvrir le "Package Manager Console" pour le projet "People.API" et ex�cuter la command "Update-Database" (attention, dans ce cas l�, la base de donn�es sera vide)

### Utilisation

Pour utiliser l'api, il faut se munir de postman et importer la collection du fichier "People.postman_collection.json".