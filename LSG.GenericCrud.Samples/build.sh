rm -fR bin
dotnet build -c Release
dotnet publish -c Release -o bin/app
# cp Dockerfile bin/app

docker build -t lsg.genericcrud.samples .

docker tag lsg.genericcrud.samples registry.heroku.com/lsg-genericcrud-samples-demo/web
heroku container:push web -a lsg-genericcrud-samples-demo             
heroku container:release web -a lsg-genericcrud-samples-demo
