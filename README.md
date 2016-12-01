#Windows App Studio Xamarin Libraries

This repository contains the source code of the libraries used in the Xamarin version of the apps generated with [Windows App Studio](http://appstudio.windows.com).

A sample implementation of these libraries is available in the Windows Store: [Windows App Studio Uwp Samples](https://www.microsoft.com/en-us/store/apps/windows-app-studio-uwp-samples/9nblggh4r0w1). You can see the implementation details in the [source code](https://github.com/wasteam/waslibs/tree/master/samples).

There are two libraries: DataProviders.Xamarin and Xamarin. The libraries are also available as Nuget packages. 
```
```
# Table of Contents <a name="table-of-contents"><a>
- [Data Providers Library](#data-providers)
    - [Facebook](#facebook)
    - [Twitter](#twitter)
    - [Flickr](#flickr)
    - [YouTube](#youtube)
    - [WordPress](#wordpress)
    - [Rss](#rss)
    - [Bing](#bing)
    - [LocalStorage](#local-storage)
    - [REST API](#rest-api)

#Data Providers Library <a name="data-providers"></a>
This library contains the implementation of all the data sources available in 
Windows App Studio apps.

This is a Portable Class Library that can be used in:
- Xamarin
and, with the "UWP" compilation directive : 
- Windows 10

Among the Data Provider classes implemented in Windows App Studio, those which access third party data, help handling the complexity of each particular provider (API calls, authentication and authorization requirements, data parsing, etc.) so provides a smooth and uniform way to access the content from these providers.  All Data Providers take advantage of the AppCache service to improve the App performance.

##Facebook Data Provider <a name="facebook"></a> 
The FacebookDataProvider allow to retrieve Facebook data through its API. You have to be registered in Facebook Apps to be able to interact with the Facebook API and you must obtain an AppId and AppSecret to configure the data access. Finally, you must use the Page ID from what you want to get the information.

*Further info*  
https://developers.facebook.com/apps/  
https://developers.facebook.com/docs/graph-api/using-graph-api  

*View Code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/Facebook  

##Twitter Data Provider <a name="twitter"></a>
The TwitterDataProvider retrieve data using the Twitter API. To be able to request Twitter data, the user must be registered in Twitter Apps and obtain a ConsumerSecret, an AccessToken and an AccessTokenSecret. This Data Provider can retrieve the user TimeLine or gather data by Twitter User Name or Hashtag.
*Further info*  
https://apps.twitter.com/  
https://dev.twitter.com/rest/public  

*View Code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/Twitter

##Flickr Data Provider <a name="flickr"></a>
The FlickrDataProvider gets images using the Flickr API. You can retrieve content based on tags or you can access the images from a Flickr account by using the UserID. To resolve which UserID is assigned to a certain Flickr account use http://idgettr.com/

*Further info*  
https://www.flickr.com/services/feeds/  
http://idgettr.com/  

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/Flickr  

##YouTube Data Provider <a name="youtube"></a>
The YouTube Data Provider retrieve data through the YouTube API by using the YouTube ChannelID, the PlaylistID, or a search term. To be able to request data using this provider, you must be registered for Google Developers Console and get an API Key.

*Further info*  
https://console.developers.google.com/project
https://dev.twitter.com/rest/public

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/YouTube

##WordPress Data Provider <a name="wordpress"></a>
The WordPress Data Provider retrieve data from the Word Press blog configured in the property WordPressQuery. You can configure the search in the Data Provider by using one of the following options: Posts, Categories or Tags. The information is read in JSON format and transformed to the WordPressSchema entity. This Data Provider relies on the REST API to access the source content. If the target blog is self-hosted (not in Wordpress.com) it must have the JetPack plug-in installed and the JSON API enabled.

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/WordPress

##Rss Data Provider <a name="rss"></a>
The RssDataProvider retrieve information from the configured RSS Url, the data is read form the source in XML format and transformed to a RssSchema entity.

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/Rss

##Bing Data Provider <a name="bing"></a>
The Bing DataProvider allows you to retreive Microsoft Bing web search engine results direct to your App.

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/Bing

##LocalStorage Data Provider <a name="local-storage"></a>
This Data Provider access data from the LocalStorage. You can configure which file will be used as content source. The information, stored in JSON format, is transformed to the specified data type.

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/LocalStorage

##REST API Data Provider <a name="rest-api"></a>
Among the Data Provider classes implemented in Windows App Studio, those which access third party data, help handling the complexity of each particular provider (API calls, authentication and authorization requirements, data parsing, etc.) so provides a smooth and uniform way to access the content from these providers.  All Data Providers take advantage of the AppCache service to improve the App performance.

The RestApiDataProvider retrieve information from the configured endpoint Url. You can configure de pagination type and items per page.

*View code*  
https://github.com/wasteam/waslibs/tree/master/src/AppStudio.DataProviders/RestApi

