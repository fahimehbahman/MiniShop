#build stage 
FROM node:20 AS build
WORKDIR /app

#copy all file which start with package and is .json from your local to image 
COPY package*.json ./

# install all package which copy in previous step 
RUN npm install


COPY . .
#this execution cammand is only for buit time 
RUN npm run build

#runtimestage 
FROM nginx:alpine

COPY --from=build /app/dist /usr/share/nginx/html

# this is not important to mention it in docker it is just for documentation , the main port put in docker.compose
EXPOSE 80

# if we dont have any command CMD will execute but it we have commands , it wont be execute 
CMD ["nginx", "-g", "daemon off;"]