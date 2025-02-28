// importing goal model
import MacroLog from "../models/macroLog.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroLog = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    // logs the food
    await new MacroLog(req.body).save()
        .then(async (macroLog) => {
            // returns the logged food
            await macroLog.populate('food');
            res.status(201).json({ msg: 'Food logged', macroLog })
        })
        .catch((error) => {
            // logs and returns status 500 if error -> food log not created
            console.log(error)
            res.status(500).json({ msg: 'Unable to log food' })
        })
}

// get method - gets all food logs for day or multiple days
export const getMacroLogDayOrDays = async (req, res) => {

    // sets search start and end date depending on if theres query params present
    let searchDate = null;
    let searchDateEnd = null;
    if (Object.keys(req.query).length > 0) {
        searchDate = new Date(req.query.startDate);
        searchDateEnd = new Date(req.query.endDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);
    } else {
        searchDate = new Date();
        searchDate.setUTCHours(0, 0, 0, 0);
        searchDateEnd = new Date(searchDate);
        searchDateEnd.setUTCDate(searchDateEnd.getUTCDate() + 1);
    }

    // finds macro log between two dates and the has user
    await MacroLog.find({
        user: req.user.id,
        date: {
            $gte: searchDate,
            $lt: searchDateEnd
        }
    }).populate('food')
        .then((macroLog) => {
            res.status(200).json({ msg: 'Found', macroLog });
        })
        .catch((error) => {
            // logs and returns status 500 error if failed or no logs found
            console.log(error)
            res.status(500).json({ msg: "Unable to find any macro logs" })
        })
}

// delete method - deletes a item from food log
export const deleteMacroLog = async (req, res) => {
    // finds and deletes food log item
    await MacroLog.findOneAndDelete(
        { _id: req.params.id, 
            user: req.user.id 
        }).populate('food')
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
    // finds macrolog from id and user id
    await MacroLog.findOne(
        { _id: req.params.id, 
            user: req.user.id 
        })
        .then(async (macroLog) => {
            // returns 404 if log isn't found | 200 if log is found
            if (!macroLog) {
                return res.status(404).json({ msg: "Macro log not found" });
            }

            // updates user with request body
            Object.keys(req.body).forEach(key => {
                macroLog[key] = req.body[key];
            });

            // runs save function to make sure password gets hashed
            const updatedMacroLog = await macroLog.save();
            await updatedMacroLog.populate('food');
            res.status(200).json({ msg: "Macro log updated", macroLog: updatedMacroLog })
        })
        .catch((error) => {
            // logs and returns status 500 if error 
            console.log(error)
            res.status(500).json({ msg: "Unable to update macro log" })
        });
};