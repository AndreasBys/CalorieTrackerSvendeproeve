// Import models
import MacroLogLegacy from "../models/macroLogLegacy.js";
import MacroLog from "../models/macroLog.js";
import FoodInDish from "../models/foodInDish.js";

export const saveMacroLog = async (req, res) => {
    try {
        // Konverter dato fra request til en UTC-dato med nulstillede timer
        let searchDate = new Date(req.body.date);
        searchDate.setUTCHours(0, 0, 0, 0);

        // Definer slutningen af dagen for søgningen
        let searchDateEnd = new Date(searchDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);

        // Find alle MacroLogs for den givne dato og bruger
        const macroLogs = await MacroLog.find({
            user: req.user.id,
            date: { $gte: searchDate, $lt: searchDateEnd }
        }).populate('food'); // Populer 'food' feltet for at få næringsindhold direkte

        // Hvis der ikke findes nogen logs, returnér en 404-fejl
        if (!macroLogs.length) {
            return res.status(404).json({ msg: 'No macro logs found for the given date' });
        }

        // Initialiser variabler til at gemme de samlede makronæringsstoffer
        let totalCalories = 0, totalProtein = 0, totalCarbs = 0, totalFat = 0;

        // Gennemgå alle brugerens logs
        for (const log of macroLogs) {
            if (log.food) {
                // Beregn næringsværdier baseret på vægt
                totalCalories += (log.food.calories * log.weight) / 100;
                totalProtein += (log.food.protein * log.weight) / 100;
                totalCarbs += (log.food.carbonhydrates * log.weight) / 100;
                totalFat += (log.food.fat * log.weight) / 100;
            } else if (log.dish) {
                // Hvis loggen refererer til en ret, find alle ingredienserne i retten
                const foodInDish = await FoodInDish.find({ dish: log.dish._id }).populate('food');
                
                // Beregn den samlede vægt af retten
                const totalDishWeight = foodInDish.reduce((sum, item) => sum + item.weight, 0);

                // Initialiser variabler til at beregne rettens næringsindhold
                let dishCalories = 0, dishProtein = 0, dishCarbs = 0, dishFat = 0;

                // Gennemgå hver ingrediens i retten og akkumuler næringsindholdet
                for (const item of foodInDish) {
                    dishCalories += (item.food.calories * item.weight) / 100;
                    dishProtein += (item.food.protein * item.weight) / 100;
                    dishCarbs += (item.food.carbonhydrates * item.weight) / 100;
                    dishFat += (item.food.fat * item.weight) / 100;
                }

                // Beregn vægtfaktoren for den spiste del af retten
                const weightFactor = log.weight / totalDishWeight;

                // Justér næringsindholdet ud fra den portion brugeren har spist
                totalCalories += dishCalories * weightFactor;
                totalProtein += dishProtein * weightFactor;
                totalCarbs += dishCarbs * weightFactor;
                totalFat += dishFat * weightFactor;
            }
        }

        // Opret en ny MacroLogLegacy-post med de beregnede værdier
        const legacyLog = await new MacroLogLegacy({
            date: searchDate,
            user: req.user.id,
            calories: totalCalories,
            carbonhydrates: totalCarbs,
            protein: totalProtein,
            fat: totalFat
        }).save();

        // Slet de gamle MacroLogs efter de er gemt i MacroLogLegacy
        await MacroLog.deleteMany({
            user: req.user.id,
            date: { $gte: searchDate, $lt: searchDateEnd }
        });

        // Returnér succesrespons med de gemte data
        res.status(201).json({ msg: 'MacroLogLegacy saved', legacyLog });

    } catch (error) {
        // Log fejl og returnér en serverfejl
        console.error(error);
        res.status(500).json({ msg: 'Unable to save macro log legacy' });
    }
};
