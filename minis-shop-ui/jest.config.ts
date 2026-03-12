import type { Config } from "jest";

const config: Config = {
  preset: "ts-jest",
  testEnvironment: "jsdom",

  moduleFileExtensions: ["ts", "tsx", "js"],

  testMatch: [
    "**/*.test.ts",
    "**/*.test.tsx"
  ],

  moduleNameMapper: {
    "\\.(css|scss)$": "identity-obj-proxy"
  },

  setupFilesAfterEnv: ["<rootDir>/jest.setup.ts"]
};

export default config;