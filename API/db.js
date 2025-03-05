import mongoose from "mongoose";
import config from "./config.js";

const dbConnect = async () => {
    try {
        await mongoose.connect(config.MONGODB_URL, {
            autoIndex: true,  // Automatiser indeksoprettelse
        });
        console.log("Connected to MongoDB!");
    } catch (error) {
        console.error("MongoDB connection error:", error);
        process.exit(1); // Stop appen ved fejl
    }
};

export default dbConnect; // Eksportér forbindelsesfunktionen
