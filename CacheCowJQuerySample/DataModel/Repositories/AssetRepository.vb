Public Class AssetRepository
    Implements iAsset

    Public Function create_asset(asset As asset) As asset Implements iAsset.create_asset

        'Set up a 'database' for the objects - in this case, simply an application variable.
        If IsNothing(HttpContext.Current.Application("asset")) Then
            HttpContext.Current.Application("assetList") = New List(Of asset)
        End If

        CType(HttpContext.Current.Application("assetList"), List(Of asset)).Add(asset)

        Return asset

    End Function

    Public Function update_asset(asset As asset) As asset Implements iAsset.update_asset

    End Function

    Public Function get_asset(id As String) As asset Implements iAsset.get_asset
        Return CType(HttpContext.Current.Application("assetList"), List(Of asset)).Where(Function(x) x.id.Equals(id)).FirstOrDefault()
    End Function

    Public Function get_all_assets() As IQueryable(Of asset) Implements iAsset.get_all_assets
        Return CType(HttpContext.Current.Application("assetList"), List(Of asset))
    End Function

End Class
