Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Public Class barcodeController
    Inherits ApiController


    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As String) As HttpResponseMessage

        If id = 0 Then
            Return Request.CreateResponse(HttpStatusCode.OK, New asset)
        Else
            Return Request.CreateResponse(HttpStatusCode.OK, (CType(HttpContext.Current.Application("db"), List(Of asset)).AsQueryable.Where(Function(x) x.barcode.Equals(id))))
        End If

    End Function

End Class
