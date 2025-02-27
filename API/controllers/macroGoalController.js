// importing goal model
import MacroGoal from "../models/macroGoal.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroGoal = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    // if the user isn't admin or no userId is found -> error returns
    if (!(req.user.admin || req.user.id == req.body.user))
        return res.status(401).json({ message: 'No access' });

    // sets startDate to current date & time
    req.body.startDate = new Date();
    req.body.startDate.setHours(0, 0, 0, 0);

    // finds the latest goal for the user
    const latestMacroGoal = await MacroGoal.findOne({ user: req.user.id }).sort({ startDate: -1 })
    try {
        // if there's a goal for the current day it updates it
        if (latestMacroGoal.startDate.getTime() == req.body.startDate.getTime()) {
            Object.keys(req.body).forEach(key => {
                latestMacroGoal[key] = req.body[key];
            });

            const updatedMacroGoal = await latestMacroGoal.save();
            res.status(200).json({ msg: "goal updated", user: updatedMacroGoal })
        }
    } catch (error) {
        // saves the new goal to database
        await new MacroGoal(req.body).save()
            .then((goal) => {
                // returns the created goal
                res.status(201).json({ msg: 'goal saved', goal })
            })
            .catch((error) => {
                // logs and returns status 500 if error -> goal not created
                console.log(error)
                res.status(500).json({ msg: 'unable to create new goal' })
            })
    }
}