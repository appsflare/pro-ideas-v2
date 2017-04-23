FROM microsoft/dotnet:1.1-sdk
WORKDIR /app
RUN mkdir /src -p
ADD src /src
RUN cd /src/ProIdeas.UI && \
	dotnet publish -o /app && \
	rm /src -rf 

EXPOSE 80
ENTRYPOINT ["dotnet", "ProIdeas.UI.dll"]
