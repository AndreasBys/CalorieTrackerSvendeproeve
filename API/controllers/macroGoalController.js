// importing goal model
import MacroGoal from "../models/macroGoal.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroGoal = async (req, res) => {
    // sets the user variable to the id of the user making the request
    const userId = req.user.id
    req.body.user = userId;

    // sets startDate to current date & time
    req.body.startDate = new Date();
    req.body.startDate.setUTCHours(0, 0, 0, 0);

    // finds the latest goal for the user
    await MacroGoal.findOne({ startDate: req.body.startDate })
        .then(async (latestMacroGoal) => {
            // if there's a goal for the current day it updates it
            Object.keys(req.body).forEach(key => {
                latestMacroGoal[key] = req.body[key];
            });

            const updatedMacroGoal = await latestMacroGoal.save();
            res.status(200).json({ msg: "goal updated", user: updatedMacroGoal })
        })
        .catch(async () => {
            // adds end date to previous goal
            await MacroGoal.findOneAndUpdate({ user: userId },
                // sets end date to the day before the new goal
                { endDate: new Date(req.body.startDate)
                    .setUTCDate(req.body.startDate.getUTCDate() - 1) },
            ).sort({ startDate: -1 })

            // saves the new goal
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
        });
}

// get method - gets the current goal for the user
export const getCurrentGoal = async (req, res) => {
    // gets user id from request params
    const userId = req.user.id

    // finds goal through userId and sort it to the latest goal
    MacroGoal.findOne({ user: userId }).sort({ startDate: -1 })
        .then((goal) => {
            // returns status 200 and the users last goal
            res.status(200).json({ goal: goal })
        })
        .catch((error) => {
            // logs and returns status 500 error if failed or no goals found
            console.log(error)
            res.status(500).json({ msg: "unable to get goal" })
        })
};

// get method - gets all goals between two dates
export const getGoalsBetweenDates = async (req, res) => {
    // finds goals between two dates and the has user
    MacroGoal.find({
        user: req.user.id,
        $and: [
            { startDate: { $lte: new Date(req.query.endDate) } },
            { $or: [{ endDate: { $gte: new Date(req.query.startDate) } }, { endDate: { $exists: false } }] }
        ]
    })
    .then((goals) => {
        res.status(200).json({ goals: goals});
    })
    .catch((error) => {
        // logs and returns status 500 error if failed or no goals found
        console.log(error)
        res.status(500).json({ msg: "unable to find any goals" })
    })
};