FROM microsoft/dotnet:1.1-sdk
RUN mkdir /build -p && \
	mkdir /app -p
ADD src /build/src
RUN cd /build/src/ProIdeas.UI && \
	dotnet publish -o /app && \
	rm /build -rf 

WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "ProIdeas.UI.dll"]
