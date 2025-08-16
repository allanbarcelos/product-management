# Application de Gestion de Produits
[English](README.md) | Français

## Aperçu
L'**Application de Gestion de Produits** est une application web développée avec ASP.NET Core 8.0, Blazor Server et SQLite pour gérer des produits. Elle permet d'ajouter, modifier, supprimer et lister des produits avec filtrage et pagination. L'application prend en charge la localisation (anglais et français), intègre Azure AD pour l'authentification et inclut des tests unitaires pour le service de produits.

## Fonctionnalités
- **Gestion des Produits** :
  - Ajouter de nouveaux produits avec nom, description et prix.
  - Modifier des produits existants.
  - Supprimer des produits.
  - Lister les produits avec filtrage par nom et pagination.
- **Localisation** :
  - Supporte les langues anglaise (`en`) et française (`fr`).
  - Interface utilisateur localisée à l'aide de fichiers de ressources.
- **Authentification** :
  - Intégration avec Azure Active Directory (Azure AD) pour une authentification sécurisée.
  - Contrôle d'accès basé sur les rôles pour restreindre la gestion des produits aux utilisateurs autorisés.
- **Base de Données** :
  - Utilise SQLite comme base de données avec Entity Framework Core.
  - Initialisation de la base de données avec des données d'exemple au démarrage.
- **Tests Unitaires** :
  - Tests unitaires complets pour le `ProductService` utilisant xUnit, FluentAssertions et une base de données en mémoire.
- **Blazor Server** :
  - Composants d'interface utilisateur interactifs pour une expérience utilisateur réactive.

## Prérequis
Pour exécuter l'application, assurez-vous d'avoir installé les éléments suivants :
- [SDK .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (ou version ultérieure) ou un autre IDE comme Visual Studio Code
- [SQLite](https://www.sqlite.org/download.html) (optionnel, pour visualiser la base de données directement)
- Un locataire Azure AD pour l'authentification (configurez dans `appsettings.json`)

## Instructions de Configuration
1. **Cloner le Dépôt** :
   ```bash
   git clone <url-du-dépôt>
   cd ProductManagementApp
   ```

2. **Configurer Azure AD** :
   - Mettez à jour le fichier `appsettings.json` dans le projet `ProductManagementApp` avec votre configuration Azure AD :
     ```json
     "AzureAd": {
       "Instance": "https://login.microsoftonline.com/",
       "Domain": "<votre-domaine>",
       "TenantId": "<votre-tenant-id>",
       "ClientId": "<votre-client-id>",
       "CallbackPath": "/signin-oidc"
     }
     ```
   - Assurez-vous que l'application est enregistrée dans Azure AD et que les permissions nécessaires sont accordées.

3. **Configurer la Base de Données** :
   - L'application utilise SQLite, et le fichier de base de données (`app.db`) est créé automatiquement dans le répertoire du projet.
   - Les données d'exemple sont insérées au démarrage de l'application via la classe `DbSeeder`.

4. **Installer les Dépendances** :
   - Restaurez les packages NuGet :
     ```bash
     dotnet restore
     ```

5. **Exécuter l'Application** :
   - Avec Visual Studio :
     - Ouvrez la solution (`ProductManagementApp.sln`) dans Visual Studio.
     - Définissez `ProductManagementApp` comme projet de démarrage.
     - Appuyez sur `F5` ou cliquez sur "Démarrer" pour exécuter l'application.
   - Avec la ligne de commande :
     ```bash
     cd ProductManagementApp
     dotnet run
     ```
   - L'application sera disponible à `https://localhost:5001` ou `http://localhost:5000` (selon votre configuration).

6. **Accéder à l'Application** :
   - Naviguez vers `/products` pour voir la liste des produits.
   - Utilisez `/add-product` pour créer un nouveau produit (nécessite une authentification).
   - Utilisez `/edit-product/{id}` pour modifier un produit existant (nécessite une authentification).
   - Connectez-vous avec vos identifiants Azure AD pour accéder aux fonctionnalités protégées.

## Exécution des Tests
La solution inclut un projet de test (`ProductManagementTests`) avec des tests unitaires pour le `ProductService`.

1. **Exécuter les Tests dans Visual Studio** :
   - Ouvrez la solution dans Visual Studio.
   - Ouvrez l'**Explorateur de Tests** (Test > Explorateur de Tests).
   - Cliquez sur **Exécuter Tous les Tests** pour lancer tous les tests unitaires.

2. **Exécuter les Tests via la Ligne de Commande** :
   - Naviguez vers le répertoire du projet de test :
     ```bash
     cd ProductManagementTests
     ```
   - Exécutez les tests avec la commande `dotnet test` :
     ```bash
     dotnet test
     ```

3. **Couverture des Tests** :
   - Le projet de test utilise `coverlet.collector` pour collecter les données de couverture de code.
   - Pour générer un rapport de couverture :
     ```bash
     dotnet test --collect:"XPlat Code Coverage"
     ```
   - Le rapport de couverture sera généré dans le répertoire `TestResults`.

## Structure du Projet
- **ProductManagementApp** :
  - Projet principal de l'application web.
  - Contient les composants Blazor (`Products.razor`, `AddProduct.razor`, `EditProduct.razor`).
  - Configure les services, les middlewares et le contexte de la base de données dans `Program.cs`.
- **ProductManagementTests** :
  - Projet de test unitaire pour tester le `ProductService`.
  - Utilise xUnit, FluentAssertions et une base de données en mémoire pour les tests.
- **Fichiers Clés** :
  - `ProductService.cs` : Gère la logique métier pour les opérations sur les produits.
  - `AppDbContext.cs` : Contexte Entity Framework Core pour la base de données SQLite.
  - `Resources.resx` : Fichiers de ressources pour la localisation en anglais et français.
  - `DbSeeder.cs` : Initialise la base de données avec des données d'exemple.

## Localisation
- L'application prend en charge les langues anglaise et française.
- Changez de langue en définissant la culture dans le navigateur ou via des paramètres de requête (par exemple, `?culture=fr`).
- Les chaînes localisées sont définies dans `Resources.resx` (anglais) et `Resources.fr.resx` (français).

## Remarques
- L'application utilise Blazor Server pour l'interactivité. Assurez-vous d'avoir une connexion stable au serveur pour des performances optimales.
- Le `@rendermode InteractiveServer` est commenté dans les composants Razor pour permettre une configuration flexible du mode de rendu.
- Assurez-vous que Azure AD est correctement configuré pour éviter les problèmes d'authentification.
- Le fichier de base de données SQLite (`app.db`) est créé dans le répertoire racine du projet. Sauvegardez ce fichier avant d'apporter des modifications importantes.

## Résolution des Problèmes
- **Problèmes d'Authentification** : Vérifiez les paramètres Azure AD dans `appsettings.json` et assurez-vous que l'utilisateur a les permissions correctes.
- **Problèmes de Base de Données** : Vérifiez que la chaîne de connexion SQLite dans `appsettings.json` est correcte et que le fichier de base de données est accessible.
- **Échecs des Tests** : Assurez-vous que la base de données en mémoire est correctement configurée et qu'aucune dépendance externe n'interfère.

## Contribution
Les contributions sont les bienvenues ! Veuillez soumettre une demande de tirage ou ouvrir un ticket pour tout bogue ou demande de fonctionnalité.

## Licence
Ce projet est sous licence MIT.