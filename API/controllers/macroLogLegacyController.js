import MacroLogLegacy from "../models/macroLogLegacy.js";
import MacroLog from "../models/macroLog.js";
import FoodInDish from "../models/foodInDish.js";
import User from "../models/user.js";

export const saveAllMacroLogs = async () => {
    try {
        let today = new Date();
        today.setUTCHours(0, 0, 0, 0);

        let tomorrow = new Date(today);
        tomorrow.setUTCDate(tomorrow.getUTCDate() + 1);

        // Find alle brugere i systemet
        const users = await User.find().select("_id");

        for (const user of users) {
            const macroLogs = await MacroLog.find({
                user: user._id,
                date: { $gte: today, $lt: tomorrow }
            }).populate('food');

            if (!macroLogs.length) continue; // Spring brugeren over, hvis der ikke er logs

            let totalCalories = 0, totalProtein = 0, totalCarbs = 0, totalFat = 0;

            for (const log of macroLogs) {
                if (log.food) {
                    totalCalories += (log.food.calories * log.weight) / 100;
                    totalProtein += (log.food.protein * log.weight) / 100;
                    totalCarbs += (log.food.carbonhydrates * log.weight) / 100;
                    totalFat += (log.food.fat * log.weight) / 100;
                } else if (log.dish) {
                    const foodInDish = await FoodInDish.find({ dish: log.dish._id }).populate('food');
                    const totalDishWeight = foodInDish.reduce((sum, item) => sum + item.weight, 0);

                    let dishCalories = 0, dishProtein = 0, dishCarbs = 0, dishFat = 0;

                    for (const item of foodInDish) {
                        dishCalories += item.food.calories * item.weight / 100;
                        dishProtein += item.food.protein * item.weight / 100;
                        dishCarbs += item.food.carbonhydrates * item.weight / 100;
                        dishFat += item.food.fat * item.weight / 100;
                    }

                    const weightFactor = log.weight / totalDishWeight;
                    totalCalories += dishCalories * weightFactor;
                    totalProtein += dishProtein * weightFactor;
                    totalCarbs += dishCarbs * weightFactor;
                    totalFat += dishFat * weightFactor;
                }
            }

            await new MacroLogLegacy({
                date: today,
                user: user._id,
                calories: totalCalories,
                carbonhydrates: totalCarbs,
                protein: totalProtein,
                fat: totalFat
            }).save();

            await MacroLog.deleteMany({
                user: user._id,
                date: { $gte: today, $lt: tomorrow }
            });
        }

        console.log("All macro logs have been saved and deleted successfully");

    } catch (error) {
        console.error("Error processing macro logs:", error);
    }
};
