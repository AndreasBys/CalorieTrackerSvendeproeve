// importing express module
import express from 'express'
import { authenticate } from '../middleware/authenticator.js';
import { createMacroLog, getMacroLogDayOrDays } from '../controllers/macroLogController.js';

// creates new router from express module
const router = express.Router();

// defining routes
router.post('/', authenticate, createMacroLog);
router.get('/', authenticate, getMacroLogDayOrDays);

// exports router as default
export default router;