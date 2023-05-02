using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderModels();
        }

        static void RenderModels()
        {
            const long workspaceId = 81590;
            const string apiKey = "5a239e76-dbb1-499a-a746-63c708f63f1f";
            const string apiSecret = "e37a715e-c13e-400a-b2a0-0e2bfd2c8c43";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);

            Workspace workspace = new Workspace("Emerging Software Architectures - C4 Model - FurniView", "Sistema de Pre visualización de muebles con Realidad Aumentada");

            ViewSet viewSet = workspace.Views;

            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem furnitureViewSystem = model.AddSoftwareSystem("Sistema de compra de muebles para el hogar con realidad aumentada", "Permite la compra de muebles para su hogar con la opción de poder previsualizarlos en un espacio determinado.");
            SoftwareSystem vuforia = model.AddSoftwareSystem("Vuforia", "Plataforma que ofrece una REST API de desarrollo de aplicaciones de Realidad Aumentada.");
            SoftwareSystem googlePay = model.AddSoftwareSystem("Google Payment Api", "Permite a los clientes realizar sus pagos con cualquier método de pago que tengan guardados en sus cuentas de google");

            Person client  = model.AddPerson("Cliente", "Cliente peruano.");
            Person vendor  = model.AddPerson("Vendedor", "Vendedor peruano.");
            Person admin = model.AddPerson("Admin", "User Admin.");
            

            client.Uses(furnitureViewSystem, "Utiliza la plataforma con el propósito de una pre visualización de muebles en su hogar");
            admin.Uses(furnitureViewSystem, "Realiza modificaciones en el sistema cuando este lo vea necesario");
            vendor.Uses(furnitureViewSystem,
                "Utiliza la plataforma para exponer sus muebles al público");

            furnitureViewSystem.Uses(googlePay, "Usa el Api de pago de Google Pay");
            furnitureViewSystem.Uses(vuforia, "Usa la API de vuforia");

            // Tags
            client.AddTags("Cliente");
            admin.AddTags("Admin");
            vendor.AddTags("Vendedor");
            furnitureViewSystem.AddTags("SistemaMonitoreo");
            vuforia.AddTags("GoogleMaps");
            googlePay.AddTags("AircraftSystem");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Cliente") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Admin") { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Vendedor"){Background = "#6cd33c", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaMonitoreo") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleMaps") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("AircraftSystem") { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
            
            SystemContextView contextView = viewSet.CreateSystemContextView(furnitureViewSystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // 2. Diagrama de Contenedores
            Container mobileApplication = furnitureViewSystem.AddContainer("Mobile App", "Permite a los usuarios visualizar con la ayuda de su cámara muebles en realidad aumentada en su hogar.", "Kotlin");
            Container webApplication = furnitureViewSystem.AddContainer("Web App", "Permite a los usuarios subir fotos en donde puedan mover los muebles a su seleccion con ayuda de realidad aumentada.", "JavaScript");
            Container landingPage = furnitureViewSystem.AddContainer("Landing Page", "", "Html css");
            Container apiRest = furnitureViewSystem.AddContainer("API REST", "API Rest", "C# .NET CORE 6.0 port 8080");

            Container furnitureLoadContext = furnitureViewSystem.AddContainer("Furniture Load Context", "Bounded Context de Furniture", "C# .NET CORE 6.0");
            Container subscriptionContext = furnitureViewSystem.AddContainer("Subscription Context", "Bounded Context de Subscription", "C# .NET CORE 6.0");
            Container accountContext = furnitureViewSystem.AddContainer("Account Context", "Bounded Context de cuentas", "C# .NET CORE 6.0");
            Container paymentContext = furnitureViewSystem.AddContainer("Payment Context", "Bounded Context de pasarela de pagos", "C# .NET CORE 6.0");
            Container orderContext = furnitureViewSystem.AddContainer("Order Context", "Bounded Context de órdenes", "C# .NET CORE 6.0");
            Container vrVisualizationContext = furnitureViewSystem.AddContainer("VR visualization Context", "Bounded Context de visualización de muebles por realidad virtual", "C# .NET CORE 6.0");


            Container database = furnitureViewSystem.AddContainer("Database", "", "SqlServer");
            
            client.Uses(mobileApplication, "Consulta");
            client.Uses(webApplication, "Consulta");
            client.Uses(landingPage, "Consulta");

            admin.Uses(mobileApplication, "Consulta");
            admin.Uses(webApplication, "Consulta");
            admin.Uses(landingPage, "Consulta");
            
            vendor.Uses(mobileApplication, "Consulta");
            vendor.Uses(webApplication, "Consulta");
            vendor.Uses(landingPage, "Consulta");

            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(paymentContext, "", "");
            apiRest.Uses(subscriptionContext, "", "");
            apiRest.Uses(accountContext, "", "");
            apiRest.Uses(furnitureLoadContext, "", "");
            apiRest.Uses(orderContext, "", "");
            apiRest.Uses(vrVisualizationContext, "", "");

            paymentContext.Uses(database, "", "");
            subscriptionContext.Uses(database, "", "");
            accountContext.Uses(database, "", "");
            furnitureLoadContext.Uses(database, "", "");
            orderContext.Uses(database, "", "");
            vrVisualizationContext.Uses(database, "", "");

            vrVisualizationContext.Uses(vuforia, "API Request", "JSON/HTTPS");
            paymentContext.Uses(googlePay, "API Request", "JSON/HTTPS");

            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");

            string contextTag = "Context";

            paymentContext.AddTags(contextTag);
            subscriptionContext.AddTags(contextTag);
            accountContext.AddTags(contextTag);
            furnitureLoadContext.AddTags(contextTag);
            orderContext.AddTags(contextTag);
            vrVisualizationContext.AddTags(contextTag);

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle(contextTag) { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(furnitureViewSystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            // 3.1 Diagrama de Componentes (Vr Visualization Context)
            
            Component domainLayerBCVR = vrVisualizationContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component vrVisualizationControllerBCVR = vrVisualizationContext.AddComponent("VrVisualizationController", "REST API endpoints de visualización por VR.", "C# .NET CORE 6.0 REST Controller");

            Component vrVisualizationApplicationServiceBCVR = vrVisualizationContext.AddComponent("VrVisualizationApplicationService", "Provee métodos para el visualizacion en 3D, pertenece a la capa Application de DDD", "C# .NET CORE 6.0 Component");

            Component imagesRepositoryBCVR = vrVisualizationContext.AddComponent("3DImagesRepository", "Muebles en 3D para realidad aumentada", "C# .NET CORE 6.0 Component");

            Component vuforiaSystemFacadeBCVR = vrVisualizationContext.AddComponent("Vuforia System Facade", "", "C# .NET CORE 6.0 Component");

            apiRest.Uses(vrVisualizationControllerBCVR, "", "JSON/HTTPS");
            vrVisualizationControllerBCVR.Uses(vrVisualizationApplicationServiceBCVR, "Invoca métodos de visualizacion virtual");

            vrVisualizationApplicationServiceBCVR.Uses(domainLayerBCVR, "Usa", "");
            vrVisualizationApplicationServiceBCVR.Uses(vuforiaSystemFacadeBCVR, "Usa");
            vrVisualizationApplicationServiceBCVR.Uses(imagesRepositoryBCVR, "", "");

            imagesRepositoryBCVR.Uses(database, "", "");

            vuforiaSystemFacadeBCVR.Uses(vuforia, "JSON/HTTPS");
            
            // Tags
            domainLayerBCVR.AddTags("domainLayerBCVR");
            vrVisualizationControllerBCVR.AddTags("vrVisualizationControllerBCVR");
            vrVisualizationApplicationServiceBCVR.AddTags("vrVisualizationApplicationServiceBCVR");
            imagesRepositoryBCVR.AddTags("imagesRepositoryBCVR");
            vuforiaSystemFacadeBCVR.AddTags("vuforiaSystemFacadeBCVR");
            
            styles.Add(new ElementStyle("domainLayerBCVR") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("vrVisualizationControllerBCVR") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("vrVisualizationApplicationServiceBCVR") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("imagesRepositoryBCVR") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("vuforiaSystemFacadeBCVR") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCVR = viewSet.CreateComponentView(vrVisualizationContext, "Vr Visualization Component", "Component Diagram");
            componentViewBCVR.PaperSize = PaperSize.A4_Landscape;
            componentViewBCVR.Add(mobileApplication);
            componentViewBCVR.Add(webApplication);
            componentViewBCVR.Add(apiRest);
            componentViewBCVR.Add(database);
            componentViewBCVR.Add(vuforia);
            componentViewBCVR.AddAllComponents();

            // 3.2 Diagrama de Componentes (FurnitureLoadContext)
            
            Component domainLayerBCFL = furnitureLoadContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component furnitureLoadControllerBCFL = furnitureLoadContext.AddComponent("FurnitureLoadController", "REST API endpoints de carga de muebles.", "C# .NET CORE 6.0 REST Controller");

            Component furnitureLoadApplicationServiceBCFL = furnitureLoadContext.AddComponent("FurnitureLoadApplicationService", "Provee métodos para realizar las consultas correspondientes para la consulta de muebles", "C# .NET CORE 6.0 Component");

            Component imagesRepositoryBCFL = furnitureLoadContext.AddComponent("FurnitureLoadRepository", "Muebles en 3D almacenados en la Base de Datos", "C# .NET CORE 6.0 Component");
            
            apiRest.Uses(furnitureLoadControllerBCFL, "", "JSON/HTTPS");
            furnitureLoadControllerBCFL.Uses(furnitureLoadApplicationServiceBCFL, "Invoca métodos de consulta de muebles");

            furnitureLoadApplicationServiceBCFL.Uses(domainLayerBCFL, "Usa", "");
            furnitureLoadApplicationServiceBCFL.Uses(imagesRepositoryBCFL, "", "");

            imagesRepositoryBCFL.Uses(database, "", "");
            
            // Tags
            domainLayerBCFL.AddTags("domainLayerBCFL");
            furnitureLoadControllerBCFL.AddTags("furnitureLoadControllerBCFL");
            furnitureLoadApplicationServiceBCFL.AddTags("furnitureLoadApplicationServiceBCFL");
            imagesRepositoryBCFL.AddTags("imagesRepositoryBCFL");
            
            styles.Add(new ElementStyle("domainLayerBCFL") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("furnitureLoadControllerBCFL") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("furnitureLoadApplicationServiceBCFL") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("imagesRepositoryBCFL") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCFL = viewSet.CreateComponentView(furnitureLoadContext, "Furniture Load Component", "Component Diagram");
            componentViewBCFL.PaperSize = PaperSize.A4_Landscape;
            componentViewBCFL.Add(mobileApplication);
            componentViewBCFL.Add(webApplication);
            componentViewBCFL.Add(apiRest);
            componentViewBCFL.Add(database);
            componentViewBCFL.AddAllComponents();
            
            //3.3 Diagrama de Componentes (order)
            
            Component domainLayerBCO = orderContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component orderControllerBCO = orderContext.AddComponent("OrderController", "REST API endpoints de ordenes", "C# .NET CORE 6.0 REST Controller");

            Component orderApplicationServiceBCO = orderContext.AddComponent("OrderApplicationService", "Provee métodos para realizar ordenes", "C# .NET CORE 6.0 Component");

            Component orderRepositoryBCO = orderContext.AddComponent("OrderRepository", "Repositorio de las ordenes registradas", "C# .NET CORE 6.0 Component");
            
            apiRest.Uses(orderControllerBCO, "", "JSON/HTTPS");
            orderControllerBCO.Uses(orderApplicationServiceBCO, "Invoca métodos de ordenes");

            orderApplicationServiceBCO.Uses(domainLayerBCO, "Usa", "");
            orderApplicationServiceBCO.Uses(orderRepositoryBCO, "", "");

            orderRepositoryBCO.Uses(database, "", "");
            
            // Tags
            domainLayerBCO.AddTags("domainLayerBCO");
            orderControllerBCO.AddTags("orderControllerBCO");
            orderApplicationServiceBCO.AddTags("orderApplicationServiceBCO");
            orderRepositoryBCO.AddTags("orderRepositoryBCO");
            
            styles.Add(new ElementStyle("domainLayerBCO") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("orderControllerBCO") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("orderApplicationServiceBCO") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("orderRepositoryBCO") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCO = viewSet.CreateComponentView(orderContext, "Order Component", "Component Diagram");
            componentViewBCO.PaperSize = PaperSize.A4_Landscape;
            componentViewBCO.Add(mobileApplication);
            componentViewBCO.Add(webApplication);
            componentViewBCO.Add(apiRest);
            componentViewBCO.Add(database);
            componentViewBCO.AddAllComponents();

            //3.4 Diagrama de Componentes (subscription)
            
            Component domainLayerBCS = subscriptionContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component subscriptionControllerBCS = subscriptionContext.AddComponent("SubscriptionController", "REST API endpoints de subscripciones", "C# .NET CORE 6.0 REST Controller");

            Component subscriptionApplicationServiceBCS = subscriptionContext.AddComponent("SubscriptionApplicationService", "Provee métodos para realizar subscripciones", "C# .NET CORE 6.0 Component");

            Component subscriptionRepositoryBCS = subscriptionContext.AddComponent("SubscriptionRepository", "Repositorio de las subscripciones registradas", "C# .NET CORE 6.0 Component");
            
            apiRest.Uses(subscriptionControllerBCS, "", "JSON/HTTPS");
            subscriptionControllerBCS.Uses(subscriptionApplicationServiceBCS, "Invoca métodos de subscripciones");

            subscriptionApplicationServiceBCS.Uses(domainLayerBCS, "Usa", "");
            subscriptionApplicationServiceBCS.Uses(subscriptionRepositoryBCS, "", "");

            subscriptionRepositoryBCS.Uses(database, "", "");
            
            // Tags
            domainLayerBCS.AddTags("domainLayerBCS");
            subscriptionControllerBCS.AddTags("subscriptionControllerBCS");
            subscriptionApplicationServiceBCS.AddTags("subscriptionApplicationServiceBCS");
            subscriptionRepositoryBCS.AddTags("subscriptionRepositoryBCS");
            
            styles.Add(new ElementStyle("domainLayerBCS") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("subscriptionControllerBCS") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("subscriptionApplicationServiceBCS") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("subscriptionRepositoryBCS") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCS = viewSet.CreateComponentView(subscriptionContext, "Subscription Component", "Component Diagram");
            componentViewBCS.PaperSize = PaperSize.A4_Landscape;
            componentViewBCS.Add(mobileApplication);
            componentViewBCS.Add(webApplication);
            componentViewBCS.Add(apiRest);
            componentViewBCS.Add(database);
            componentViewBCS.AddAllComponents();
            
            //3.5 Diagrama de Componentes (account)
            
            Component domainLayerBCA = accountContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component accountControllerBCA = accountContext.AddComponent("AccountController", "REST API endpoints de cuentas", "C# .NET CORE 6.0 REST Controller");

            Component accountApplicationServiceBCA = accountContext.AddComponent("AccountApplicationService", "Provee métodos para realizar cuentas", "C# .NET CORE 6.0 Component");

            Component accountRepositoryBCA = accountContext.AddComponent("AccountRepository", "Repositorio de las cuentas registradas", "C# .NET CORE 6.0 Component");
            
            apiRest.Uses(accountControllerBCA, "", "JSON/HTTPS");
            accountControllerBCA.Uses(accountApplicationServiceBCA, "Invoca métodos de cuentas");

            accountApplicationServiceBCA.Uses(domainLayerBCA, "Usa", "");
            accountApplicationServiceBCA.Uses(accountRepositoryBCA, "", "");

            accountRepositoryBCA.Uses(database, "", "");
            
            // Tags
            domainLayerBCA.AddTags("domainLayerBCA");
            accountControllerBCA.AddTags("accountControllerBCA");
            accountApplicationServiceBCA.AddTags("accountApplicationServiceBCA");
            accountRepositoryBCA.AddTags("accountRepositoryBCA");
            
            styles.Add(new ElementStyle("domainLayerBCA") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("accountControllerBCA") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("accountApplicationServiceBCA") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("accountRepositoryBCA") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCA = viewSet.CreateComponentView(accountContext, "Account Component", "Component Diagram");
            componentViewBCA.PaperSize = PaperSize.A4_Landscape;
            componentViewBCA.Add(mobileApplication);
            componentViewBCA.Add(webApplication);
            componentViewBCA.Add(apiRest);
            componentViewBCA.Add(database);
            componentViewBCA.AddAllComponents();
            
            
            //3.6 Diagrama de Componentes (payment)
            
            Component domainLayerBCP = paymentContext.AddComponent("Domain Layer", "", "C# .NET CORE 6.0");

            Component paymentControllerBCP = paymentContext.AddComponent("PaymentController", "REST API endpoints de pago", "C# .NET CORE 6.0 REST Controller");

            Component paymentApplicationServiceBCP = paymentContext.AddComponent("PaymentApplicationService", "Provee métodos para el pago", "C# .NET CORE 6.0 Component");

            Component imagesRepositoryBCP = paymentContext.AddComponent("Payment Repository", "Repositorio de registro de pagos", "C# .NET CORE 6.0 Component");

            Component vuforiaSystemFacadeBCP = paymentContext.AddComponent("Google Payment System Facade", "", "C# .NET CORE 6.0 Component");

            apiRest.Uses(paymentControllerBCP, "", "JSON/HTTPS");
            paymentControllerBCP.Uses(paymentApplicationServiceBCP, "Invoca métodos de pasarela de pagos");

            paymentApplicationServiceBCP.Uses(domainLayerBCP, "Usa", "");
            paymentApplicationServiceBCP.Uses(vuforiaSystemFacadeBCP, "Usa");
            paymentApplicationServiceBCP.Uses(imagesRepositoryBCP, "", "");

            imagesRepositoryBCP.Uses(database, "", "");

            vuforiaSystemFacadeBCP.Uses(googlePay, "JSON/HTTPS");
            
            // Tags
            domainLayerBCP.AddTags("domainLayerBCP");
            paymentControllerBCP.AddTags("paymentControllerBCP");
            paymentApplicationServiceBCP.AddTags("paymentApplicationServiceBCP");
            imagesRepositoryBCP.AddTags("imagesRepositoryBCP");
            vuforiaSystemFacadeBCP.AddTags("vuforiaSystemFacadeBCP");
            
            styles.Add(new ElementStyle("domainLayerBCP") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("paymentControllerBCP") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("paymentApplicationServiceBCP") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("imagesRepositoryBCP") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("vuforiaSystemFacadeBCP") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentViewBCP = viewSet.CreateComponentView(paymentContext, "Payment Component", "Component Diagram");
            componentViewBCP.PaperSize = PaperSize.A4_Landscape;
            componentViewBCP.Add(mobileApplication);
            componentViewBCP.Add(webApplication);
            componentViewBCP.Add(apiRest);
            componentViewBCP.Add(database);
            componentViewBCP.Add(googlePay);
            componentViewBCP.AddAllComponents();
            
            
            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}