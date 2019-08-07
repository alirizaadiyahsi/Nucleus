module.exports = {
    root: true,
    env: {
        node: true
    },
    extends: ["plugin:vue/essential", "@vue/prettier", "@vue/typescript"],
    rules: {
        "no-console": process.env.NODE_ENV === "production" ? "error" : "off",
        "no-debugger": process.env.NODE_ENV === "production" ? "error" : "off",
        "indent": ["error", 4],
        "quotes": [2, "single", { "avoidEscape": true }],
        "eol-last": ["error", "never"]
    },
    parserOptions: {
        parser: "@typescript-eslint/parser"
    }
};
