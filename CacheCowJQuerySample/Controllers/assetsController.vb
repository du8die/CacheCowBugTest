Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Public Class assetsController
    Inherits ApiController

    ' GET api/<controller>
    Public Function GetValues() As HttpResponseMessage

        Return Request.CreateResponse(HttpStatusCode.OK, CType(HttpContext.Current.Application("db"), List(Of asset)).AsQueryable)

    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As HttpResponseMessage
        If id = 0 Then
            HttpContext.Current.Application("db") = New List(Of asset)
            Return Request.CreateResponse(HttpStatusCode.OK, New asset)
        Else
            Return Request.CreateResponse(HttpStatusCode.OK, (CType(HttpContext.Current.Application("db"), List(Of asset)).AsQueryable.Where(Function(x) x.id.Equals(id))))
        End If

    End Function

    ' POST api/<controller>
    Public Function PostValue(<FromBody()> ByVal value As asset) As HttpResponseMessage
        'Check
        Dim index As Integer

        Try
            index = CType(HttpContext.Current.Application("db"), List(Of asset)).FindIndex(Function(x) x.id.Equals(value.id))

            If index = -1 Then
                CType(HttpContext.Current.Application("db"), List(Of asset)).Add(value)
                Return Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                Return Request.CreateResponse(HttpStatusCode.Conflict, "Item already exists.")
            End If

        Catch ex As Exception
            Return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString)
        End Try

    End Function

    ' PUT api/<controller>/5
    Public Function PutValue(ByVal id As Integer, <FromBody()> ByVal value As asset) As HttpResponseMessage

        Dim index As Integer = CType(HttpContext.Current.Application("db"), List(Of asset)).FindIndex(Function(x) x.id.Equals(id))

        CType(HttpContext.Current.Application("db"), List(Of asset)).Item(index) = value

        Return Request.CreateResponse(HttpStatusCode.OK, value)

    End Function

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class
