import { saveAllMacroLogs } from '../controllers/macroLogLegacyController.js';
import config from '../config.js';
import mongoose from 'mongoose';

const dbConnect = async () => {
    try {
        await mongoose.connect(config.MONGODB_URL, {
            autoIndex: true,  // automatically build indexes
        });
        console.log('connected to db');  // log successful connection
    } catch (error) {
        console.log(error);  // log any connection errors
        process.exit(1);  // exit process with failure
    }
};

// establish connection to the database
dbConnect();

(async () => {
    console.log("Manually testing cron job...");
    await saveAllMacroLogs();
    console.log("Cron job test completed.");
})();
