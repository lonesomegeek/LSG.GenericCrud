rm -fR bin
dotnet build -c Release
dotnet publish -c Release -o bin/app
cp Dockerfile bin/app

docker build -t lsg.genericcrud.samples:5.0.1 .

docker tag lsg.genericcrud.samples:5.0.1 registry.heroku.com/lsg-genericcrud-samples-demo/web
heroku container:push web -a lsg-genericcrud-samples-demo             
heroku container:release web -a lsg-genericcrud-samples-demo