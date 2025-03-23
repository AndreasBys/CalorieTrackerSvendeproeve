import MacroLogLegacy from "../models/macroLogLegacy.js";
import MacroLog from "../models/macroLog.js";
import FoodInDish from "../models/foodInDish.js";
import User from "../models/user.js";

export const saveAllMacroLogs = async () => {
    try {
        // Sætter dato for søgning af logs og til gem
        let today = new Date();
        today.setUTCHours(0, 0, 0, 0);

        let yesterday = new Date(today);
        yesterday.setUTCDate(yesterday.getUTCDate() -1 );

        // Find alle brugere i systemet
        const users = await User.find().select("_id");

        // Gennem gå brugere og find deres logs og populater food
        for (const user of users) {
            const macroLogs = await MacroLog.find({
                user: user._id,
                date: { $gte: yesterday, $lt: today }
            }).populate('food');

            if (!macroLogs.length) continue; // Spring brugeren over, hvis der ikke er logs

            let totalCalories = 0, totalProtein = 0, totalCarbs = 0, totalFat = 0;

            // Looper igennem hver log fra hver bruger og regner makroer ud
            for (const log of macroLogs) {
                if (log.food) {
                    // Hvis loggen indeholder en fødevarer
                    totalCalories += (log.food.calories * log.weight) / 100;
                    totalProtein += (log.food.protein * log.weight) / 100;
                    totalCarbs += (log.food.carbonhydrates * log.weight) / 100;
                    totalFat += (log.food.fat * log.weight) / 100;
                } else if (log.dish) {
                    // Hvis loggen indeholder en ret
                    // Find alle fødevarer i retten og populater dem
                    const foodInDish = await FoodInDish.find({ dish: log.dish._id }).populate('food');

                    // Find total vægt af retten
                    const totalDishWeight = foodInDish.reduce((sum, item) => sum + item.weight, 0);

                    let dishCalories = 0, dishProtein = 0, dishCarbs = 0, dishFat = 0;

                    // For hver fødevare i retten, regn makroer ud
                    for (const item of foodInDish) {
                        dishCalories += item.food.calories * item.weight / 100;
                        dishProtein += item.food.protein * item.weight / 100;
                        dishCarbs += item.food.carbonhydrates * item.weight / 100;
                        dishFat += item.food.fat * item.weight / 100;
                    }

                    // Regn makroer ud for retten og tilføj til total
                    const weightFactor = log.weight / totalDishWeight;
                    totalCalories += dishCalories * weightFactor;
                    totalProtein += dishProtein * weightFactor;
                    totalCarbs += dishCarbs * weightFactor;
                    totalFat += dishFat * weightFactor;
                }
            }

            // opretter ny macroLogLegacy for brugeren
            await new MacroLogLegacy({
                date: yesterday,
                user: user._id,
                calories: totalCalories,
                carbonhydrates: totalCarbs,
                protein: totalProtein,
                fat: totalFat
            }).save();

            // sletter de gamle macro logs for brugeren
            await MacroLog.deleteMany({
                user: user._id,
                date: { $gte: yesterday, $lt: today }
            });
        }

        console.log("All macro logs have been saved and deleted successfully");

    } catch (error) {
        console.error("Error processing macro logs:", error);
    }
};

export const getMacroLogDays = async (req, res) => {
    try {
        // sets search start and end date depending on if theres query params present
        const searchDate = new Date(req.query.startDate);
        const searchDateEnd = new Date(req.query.endDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);

        // finds macro log between two dates and the has user
        const legacyLogs = await MacroLogLegacy.find({
            user: req.user.id,
            date: {
                $gte: searchDate,
                $lt: searchDateEnd
            }
        });

        // returns the logs
        res.status(200).json({ msg: 'Found following macro logs', legacyLogs });
    } catch (error) {
        // returns error if something went wrong
        res.status(500).json({ msg: "Unable to find any macro logs" });
    }
}