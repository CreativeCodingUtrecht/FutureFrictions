module.exports = {
    apps : [
        {
          name: "futurefrictions",
          script: "build/index.js",
          watch: true,
          env: {
            PORT: "3001",
            ORIGIN: "https://futurefrictions.appliedcreative.nl",
            DATAROOT: "../../data"
          }
        }
    ]
  }