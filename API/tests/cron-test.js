import { saveAllMacroLogs } from '../controllers/macroLogLegacyController.js';
import dbConnect            from '../db.js';

// establish connection to the database
dbConnect();

(async () => {
    console.log("Manually testing cron job...");
    await saveAllMacroLogs();
    console.log("Cron job test completed.");
})();
