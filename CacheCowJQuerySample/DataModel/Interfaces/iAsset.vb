Public Interface iAsset

    Function create_asset(asset As asset) As asset
    Function update_asset(asset As asset) As asset
    Function get_asset(ByVal id As String) As asset
    Function get_all_assets() As IQueryable(Of asset)

End Interface
