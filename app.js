const express = require("express");

const app = express();

const port = process.env.PORT || 3000;

app.get("/", (req, res) => {
    res.send(`
        <html>
            <head>
                <title>Azure Starter App</title>
            </head>
            <body>
                <h1>Hello from Azure App Service!</h1>
                <p>This Node.js application is running on Linux.</p>
            </body>
        </html>
    `);
});

app.listen(port, () => {
    console.log(`Server running on port ${port}`);
});