Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports CacheCow.Server

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        'CacheCow Init...
        Dim cachecow = New CachingHandler(config)
        config.MessageHandlers.Add(cachecow)

        HttpContext.Current.Application("db") = New List(Of asset)

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        config.Routes.MapHttpRoute(
            name:="Barcode",
            routeTemplate:="api/assets/barcode/{id}",
            defaults:=New With {.controller = "barcode"}
        )

    End Sub
End Module
