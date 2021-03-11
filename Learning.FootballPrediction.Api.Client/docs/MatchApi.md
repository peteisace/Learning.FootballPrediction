# IO.Swagger.Api.MatchApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**MatchSavePost**](MatchApi.md#matchsavepost) | **POST** /Match/save | 

<a name="matchsavepost"></a>
# **MatchSavePost**
> Match MatchSavePost (MatchRequest body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MatchSavePostExample
    {
        public void main()
        {
            var apiInstance = new MatchApi();
            var body = new MatchRequest(); // MatchRequest |  (optional) 

            try
            {
                Match result = apiInstance.MatchSavePost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MatchApi.MatchSavePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**MatchRequest**](MatchRequest.md)|  | [optional] 

### Return type

[**Match**](Match.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
