FROM node:18.19.0

WORKDIR /app

COPY . .

WORKDIR /app/hub

RUN npm i

RUN npm run build

# Delete source code files that were used to build the app that are no longer needed
RUN rm -rf src/ static/

USER node:node

CMD ["node","build/index.js"]