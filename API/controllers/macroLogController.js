// importing models
import MacroLog from "../models/macroLog.js"
import FoodInDish from "../models/foodInDish.js";

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroLog = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    try{
        // saves the food log
        const macroLog = await new MacroLog(req.body).save()

        // populate food details
        await macroLog.populate('food')
        res.status(201).json({ msg: 'Food logged', macroLog })

    } catch (error) {
        // returns status 500 if error
        res.status(500).json({ msg: 'Unable to log food' })
    }
}

// get method - gets all food logs for day or multiple days
export const getTodaysMacroLog = async (req, res) => {
    try {
        // Sætter start- og slutdato til dagens UTC tid
        let searchDate = new Date();
        searchDate.setUTCHours(0, 0, 0, 0);
        let searchDateEnd = new Date(searchDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);

        // Finder macroLogs for brugeren i dag
        let macroLogs = await MacroLog.find({
            user: req.user.id,
            date: { $gte: searchDate, $lt: searchDateEnd }
        })
        .populate('food') // Henter food detaljer
        .populate('dish'); // Henter dish detaljer

        // Hvis en log har en dish, henter vi dens ingredienser
        await Promise.all(
            macroLogs.map(async (log, index) => {
                if (log.dish) {
                    const foodsInDish = await FoodInDish.find({ dish: log.dish._id })
                        .populate('food')
                        .select('food weight');
        
                    macroLogs[index] = log.toObject(); // Konverter til almindeligt objekt
                    macroLogs[index].dish.foods = foodsInDish; // Tilføj foods til dish
                }
            })
        );
        

        // Returnerer opdaterede macroLogs
        res.status(200).json({ msg: 'Found', macroLogs });

    } catch (error) {
        console.log(error);
        res.status(500).json({ msg: "Unable to find any macro logs" });
    }
};

// delete method - deletes a item from food log
export const deleteMacroLog = async (req, res) => {
    // finds and deletes food log item
    await MacroLog.findOneAndDelete(
        { _id: req.params.id, 
            user: req.user.id 
        })
        .then((macroLog)=>{
            res.status(200).json({ macroLog });
        })
        .catch((error) => {
            // logs and returns status 500 error if failed or no logs found
            console.log(error)
            res.status(500).json({ msg: "unable to find any macro logs" })
        })
}

// Update method - udates a food log item
export const updateMacroLog = async (req, res) => {
    try {
        // Finder og opdaterer kun weight
        const updatedMacroLog = await MacroLog.findOneAndUpdate(
            { _id: req.params.id, user: req.user.id }, 
            { weight: req.body.weight }, 
            { new: true } // Returnerer den opdaterede version
        ).populate('food');

        // Hvis loggen ikke findes
        if (!updatedMacroLog) {
            return res.status(404).json({ msg: "Macro log not found" });
        }

        res.status(200).json({ msg: "Macro log updated", macroLog: updatedMacroLog });
    } catch (error) {
        console.log(error);
        res.status(500).json({ msg: "Unable to update macro log" });
    }
};