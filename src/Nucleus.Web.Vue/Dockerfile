

# build stage
FROM node:lts-alpine as build-stage
ARG environment
WORKDIR /app
COPY ./src/Nucleus.Web.Vue/package*.json ./
COPY ./src/Nucleus.Web.Vue/yarn.lock ./
RUN yarn install 
COPY ./src/Nucleus.Web.Vue/ .
RUN yarn run build --mode $environment ;

# production stage
FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY  ./src/Nucleus.Web.Vue/config/app.conf /etc/nginx/conf.d/app.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]